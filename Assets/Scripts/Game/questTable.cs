using DG.Tweening;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class questTable : MonoBehaviour
{
    //properties
    public string domain = "math";
    public float difficulty = .5f;

    //public variables
    public string answer = "";
    public float mathAns = 0;
    public float mTime = 1;

    //private variables
    private GameObject lastQuestion;

    //ui elements
    [SerializeField] private List<GameObject> questPrefabs = new List<GameObject>();
    [SerializeField] private List<Sprite> domSprites = new List<Sprite>();

    public void generate()
    {
        //clean-up last question
        clear();

        //generate question
        (string, string) value = ("", "");
        
        int domainId = 0;
        switch (domain)
        {
            case "math":
                branchChance = 1;
                branchCount = 0;
                var result = math(null);
                value.Item1 = result.Item1;
                value.Item2 = (Mathf.Floor(result.Item2 * 1000) / 1000f).ToString("0.###", CultureInfo.InvariantCulture);
                mathAns = result.Item2;
                domainId = 0;
                mTime = branchCount;
                break;
        }

        GameObject newQuest = Instantiate(questPrefabs[domainId], transform);
        lastQuestion = newQuest;

        //expose answer publicly
        answer = value.Item2;

        //display question

        var text = newQuest.transform.Find("text");
        var domImg = newQuest.transform.Find("genre");

        text.GetComponent<TextMeshProUGUI>().text = value.Item1;
        domImg.GetComponent<Image>().sprite = domSprites[domainId];

        //aniamtion
        newQuest.SetActive(true);
        DOTween.To(() => newQuest.GetComponent<UIScale>().relativeSize, x => newQuest.GetComponent<UIScale>().relativeSize = x, new Vector2(1, 1), .2f).SetEase(Ease.OutBack);
    }

    public void clear()
    {
        if (lastQuestion != null)
        {
            Destroy(lastQuestion);
        }
    }

    #region question generators

    private float branchChance = 1;
    public int branchCount = 0;

    private List<string> operations = new List<string>
    {
        "+", "-", "*", "/",
    };

    private (string, float) math(string lastOp)
    {
        if (Random.value < branchChance)
        {
            branchCount++;
            branchChance = Mathf.Clamp01(1 - branchCount / (3f * difficulty));

            string operation = operations[Random.Range(0, (int)Mathf.Clamp(2 + difficulty, 1, operations.Count))];
            (string, float) a = math(operation);
            (string, float) b = math(operation);

            float ans = 0;

            switch (operation)
            {
                case "+":
                    ans = a.Item2 + b.Item2;
                    break;
                case "-":
                    ans = a.Item2 - b.Item2;
                    break;
                case "*":
                    ans = a.Item2 * b.Item2;
                    break;
                case "/":
                    if (b.Item2 == 0)
                    {
                        b.Item2 = Random.Range(1, (int)Mathf.Pow(10, difficulty));
                        b.Item1 = b.Item2.ToString();
                    }
                    ans = a.Item2 / b.Item2;
                    break;
                default:
                    ans = a.Item2 + b.Item2;
                    break;
            }

            string textR = a.Item1 + " " + operation + " " + b.Item1;
            if (lastOp != null)
            {
                if (textR[0] == '(')
                    textR = "[" + textR + "]";
                else if (textR[0] == '[' || textR[0] == '{')
                    textR = "{" + textR + "}";
                else
                    textR = "(" + textR + ")";
            }

            return (textR, ans);
        }
        else
        {
            int n = Random.Range(1, (int)Mathf.Pow(10, difficulty));
            return (n.ToString("N0"), n);
        }
    }

    #endregion
}
