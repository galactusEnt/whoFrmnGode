using Coffee.UIExtensions;
using DG.Tweening;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class questionMain : MonoBehaviour
{
    //public variables
    public float difficulty = 1, time = 12;
    public int nQuest = 1;
    public string domain = "math";
    public bool fixedDomain = true;

    //private variables
    private GameObject correctChoice;

    //ui elements
    [SerializeField] private GameObject questTable, questAns, questChoice, questTimer, questTitle, confetti;
    [SerializeField] private AudioClip questSolve;

    //events
    public UnityEvent onSolve, onFail;

    private List<string> domains = new List<string>
    {
        "math",
    };

    void Start()
    {
        //setup components

        //--timer
        questTimer.GetComponent<timer>().maxTime = time;
        questTimer.GetComponent<timer>().currentTime = time;

        //animate "Question Time!"
        questTitle.GetComponent<UIScale>().relativePosition = new Vector2(.5f, 1.5f);
        questTitle.SetActive(true);
        DOTween.To(() => questTitle.GetComponent<UIScale>().relativePosition, x => questTitle.GetComponent<UIScale>().relativePosition = x, new Vector2(.5f, .5f), .3f).SetEase(Ease.OutElastic);

        Util.ExecuteAfterDelay(this, () =>
        {
            questTitle.SetActive(false);
            questTitle.GetComponent<UIScale>().relativePosition = new Vector2(.5f, 1.5f);
            showQuest();

            //start timer
            questTimer.GetComponent<timer>().startTimer();
        }, 1);
    }

    public void showQuest()
    {
        nQuest--;

        //show ui
        questTable.SetActive(true);
        questTimer.SetActive(true);


        //choosing random domain if necessary
        if (!fixedDomain)
        {
            domain = domains[Random.Range(0, domains.Count)];
        }

        //generate question
        var quest = questTable.GetComponent<questTable>();
        quest.domain = domain;
        quest.difficulty = difficulty;
        quest.generate();


        //display answer method
        float choiceChance = Mathf.Clamp(.9f * Mathf.Pow(.7f, difficulty - 1), .5f, 1);
        if (Random.value <= choiceChance)
        {
            questAns.SetActive(false);
            questChoice.SetActive(true);
        }
        else
        {
            questChoice.SetActive(false);
            questAns.SetActive(true);
        }

        string answer = quest.answer;

        if (questAns.activeSelf) //detect answer method
        {
            questAns.transform.Find("input").GetComponent<TMP_InputField>().ActivateInputField();
            return;
        }

        int imp = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            var choiceButton = questChoice.transform.Find("ans" + (i + 1).ToString());
            var cText = choiceButton.Find("text").GetComponent<TextMeshProUGUI>();
            if (i == imp)
            {
                cText.text = (i + 1).ToString() + ") " + answer;
                correctChoice = choiceButton.gameObject;
                continue;
            }

            switch (domain)
            {
                case "math":

                    //update time based on question difficulty
                    time = 6 + quest.mTime * 1.4f * Mathf.Pow(3, difficulty);

                    //choose other answers based on question generation method
                    float diffAns = quest.mathAns;

                    diffAns += Random.Range(1, 10 * (1 + (int)Mathf.Log10(quest.mathAns)) + 1) * (Random.Range(0, 2) * 2 - 1);

                    diffAns *= (Random.Range(0, 2) * 2 - 1);

                    if (diffAns - (int)diffAns < .001f || difficulty < 1.5f) //it is an integer and the difficulty is easier
                    {
                        cText.text = (i + 1).ToString() + ") " + diffAns.ToString("0.###", CultureInfo.InvariantCulture);
                        break;
                    }

                    diffAns += Random.Range(.001f, .999f) * (Random.Range(0, 2) * 2 - 1);
                    cText.text = (i + 1).ToString() + ") " + diffAns.ToString("0.###", CultureInfo.InvariantCulture);
                    break;
            }

        }
    }

    public void checkAnswer(GameObject obj)
    {
        int state = 0;

        //determine state of answer
        if (obj.transform.Find("input") == questAns.transform.Find("input")) //input field answer
        {
            TMP_InputField input = obj.transform.Find("input").GetComponent<TMP_InputField>();
            var quest = questTable.GetComponent<questTable>();

            //normalize input text string
            string inputText = input.text.TrimEnd().ToLower();

            if (inputText == quest.answer) //answer is correct!
            {
                state = 2;
            }
            else //answer is wrong
            {
                state = 0;
            }
        }
        else
        {
            //multiple choice
            if (correctChoice == obj) //answer is correct!
            {
                state = 2;
            }
            else
            {
                state = 1;
            }
        }

        //update color to match state
        if (state == 0 || state == 1)
        {
            obj.GetComponent<Image>().color = new Color(147 / 255f, 47 / 255f, 60 / 255f);
            obj.transform.Find("outline").GetComponent<UIOutline>().color = new Color(106 / 255f, 32 / 255f, 44 / 255f);
            Vector3 rot = new();
            DOTween.Shake(() => rot, x => { rot = x; obj.GetComponent<RectTransform>().rotation = Quaternion.Euler(rot); }, .4f, new Vector3(0, 0, 2), 20);
        }
        else
        {
            obj.GetComponent<Image>().color = new Color(44 / 255f, 152 / 255f, 69 / 255f);
            obj.transform.Find("outline").GetComponent<UIOutline>().color = new Color(23 / 255f, 101 / 255f, 42 / 255f);
            Vector3 rot = new();
            DOTween.Shake(() => rot, x => { rot = x; obj.GetComponent<RectTransform>().rotation = Quaternion.Euler(rot); }, .4f, new Vector3(0, 0, 2), 20);
        }

        if(state == 0) //retry input
        {
            obj.GetComponent<Image>().color = new Color(147 / 255f, 47 / 255f, 60 / 255f);
            obj.transform.Find("outline").GetComponent<UIOutline>().color = new Color(106 / 255f, 32 / 255f, 44 / 255f);

            TMP_InputField input = obj.transform.Find("input").GetComponent<TMP_InputField>();
            input.transform.Find("tArea").Find("placeholder").GetComponent<TextMeshProUGUI>().text = input.text;
            input.text = "";

            input.ActivateInputField();
            return;
        }
        if (state == 1) //wrong choice
        {
            Util.ExecuteAfterDelay(this, () => {
                obj.GetComponent<Image>().color = new Color(42 / 255f, 34 / 255f, 56 / 255f);
                obj.transform.Find("outline").GetComponent<UIOutline>().color = new Color(94 / 255f, 72 / 255f, 120 / 255f);
                endQuest(0);
            }, .5f);
            return;
        }
        if (state == 2) //correct
        {
            questTimer.GetComponent<timer>().stopTimer();

            Util.ExecuteParallel(() => {
                //play sound
                //GetComponent<AudioSource>().PlayOneShot(questSolve);

                for (int i = 1; i <= 4; i++)
                {
                    float x = Random.value;
                    float y = Random.value;

                    var newParticle = Instantiate(confetti, transform.parent);
                    newParticle.GetComponent<UIScale>().relativePosition = new Vector2(x, y);
                    newParticle.GetComponent<UIParticle>().scale = Random.Range(40, 60);
                    Util.EmitParticlesAndDestroy(newParticle.GetComponent<ParticleSystem>(), (int)(Random.Range(75, 125) * PlayerPrefs.GetInt("Particles", 3) / 3f));
                }
            });

            if (nQuest > 0)
            {
                Util.ExecuteAfterDelay(this, () => {
                    
                    obj.GetComponent<Image>().color = new Color(42 / 255f, 34 / 255f, 56 / 255f);
                    obj.transform.Find("outline").GetComponent<UIOutline>().color = new Color(94 / 255f, 72 / 255f, 120 / 255f);
                    
                    showQuest();
                    
                    questTimer.GetComponent<timer>().restartTimer();
                }, .5f);

                return;
            }

            Util.ExecuteAfterDelay(this, () => {
                
                obj.GetComponent<Image>().color = new Color(42 / 255f, 34 / 255f, 56 / 255f);
                obj.transform.Find("outline").GetComponent<UIOutline>().color = new Color(94 / 255f, 72 / 255f, 120 / 255f);
                
                endQuest(1);
            }, .5f);
            return;
        }
    }

    public void endQuest(int state)
    {

        if (state == 0) //fail
        {
            onFail.Invoke();
        }
        else
        {
            onSolve.Invoke();
        }
        Destroy(gameObject);
    }
}
