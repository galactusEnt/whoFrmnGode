using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scoreboard : MonoBehaviour
{
    //properties
    public int round, score, level, maxScore = 1, highScore = 0;

    //ui elements
    [SerializeField] private GameObject roundText, scoreText, levelText, progress, highscore;
    [SerializeField] private List<Sprite> stars = new List<Sprite>();
    [SerializeField] private GameObject starPrefab;

    //private variables
    private int lastScore = 0, tweenScore;

    public void updateUI()
    {
        bool updateHighscore = false;
        if (score > highScore && highscore != null) 
        {
            updateHighscore = true;
        } else if (highscore != null)
        {
            highscore.GetComponent<TextMeshProUGUI>().text = highScore.ToString("N0") + " pts";
        }

        lastScore = score;
        DOTween.To(
            () => tweenScore,
            x => {
                tweenScore = x;
                scoreText.GetComponent<TextMeshProUGUI>().text = tweenScore.ToString("N0") + " pts";
                if (updateHighscore)
                {
                    highscore.GetComponent<TextMeshProUGUI>().text = tweenScore.ToString("N0") + " pts";
                }
                scoreText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => {
                    scoreText.transform.DOScale(Vector3.one, 0.1f);
                });
            },
            score, .3f
            ).SetEase(Ease.InBounce);

        if (progress != null)
        {
            var progBar = progress.transform.Find("progress");
            DOTween.To(() => progBar.GetComponent<UIScale>().relativeSize, x => progBar.GetComponent<UIScale>().relativeSize = x, new Vector2(Mathf.Clamp01(((float)score) / maxScore), 1), .1f);
        }

        if (roundText != null)
        {
            roundText.GetComponent<TextMeshProUGUI>().text = "Round " + round.ToString("N0");
        }

        if (levelText != null)
        {
            levelText.GetComponent<TextMeshProUGUI>().text = "Level " + level.ToString("N0");
        }
    }

    public void updateStar(int id, int state)
    {
        if(progress == null) return;

        var star = progress.transform.Find("s" + id.ToString());
        switch(state)
        {
            case 0: //golden unobtained
                star.GetComponent<Image>().sprite = stars[0];
                star.GetComponent<Image>().color = new Color(1, 1, 1, .4f);
                star.DORotate(new Vector3(0, 0, 360), .8f, RotateMode.WorldAxisAdd).SetEase(Ease.OutExpo);
                break;
            case 1: //golden obtained
                star.GetComponent<Image>().sprite = stars[0];
                star.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                star.DORotate(new Vector3(0, 0, -360), .8f, RotateMode.WorldAxisAdd).SetEase(Ease.OutExpo);
                break;
            case 2: //void obtained
                star.GetComponent<Image>().sprite = stars[1];
                star.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                star.DORotate(new Vector3(0, 0, -720), 1.3f, RotateMode.WorldAxisAdd).SetEase(Ease.OutExpo);
                break;
        }
    }

    public void updateStar(List<float> starPos)
    {
        for (int i = 1; i <= starPos.Count; i++)
        {
            var star = Instantiate(starPrefab, progress.transform);
            star.name = $"s{i}";
            star.GetComponent<UIScale>().relativePosition = new Vector2(starPos[i-1], .5f);
        }
    }

    //unity funtions
    void Start()
    {
        updateUI();
    }
    void Update()
    {
        if (score == lastScore) return; //no need to update
        updateUI();
    }
}
