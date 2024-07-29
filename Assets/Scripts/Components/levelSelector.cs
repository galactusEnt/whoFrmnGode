using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class levelSelector : MonoBehaviour
{
    //public variables
    public GameObject currentSpawner;
    [HideInInspector] public int currentLevel = 1;
    public int collectedStars = 0;

    //ui
    [SerializeField] private GameObject infoBox, bar;
    private int maxStars = 0;
    private int lastCollectedStars = 0;
    private string currentChapter = "The Beginnings";

    void Start()
    {
        OnChapterSelect("The Beginnings");
    }

    void Update()
    {
        if (lastCollectedStars != collectedStars)
        {
            lastCollectedStars = collectedStars;
            UpdateUI();
        }
    }

    public void OnChapterSelect(string chapterName)
    {
        maxStars = 0;
        lastCollectedStars = 0;
        collectedStars = 0;

        //update levels
        var playerData = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().playerData.storyModeProgress;
        var gameData = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().gameData.storyMode;

        //find in game objs
        var levelObjs = transform.Find(chapterName);
        if (levelObjs == null) return;

        //get chapter data
        var gameLevelsData = gameData[chapterName];
        var playerLevelsData = playerData[chapterName];

        //update chapter max star capacity
        if (playerLevelsData != null) currentLevel = playerLevelsData.Count;

        for (int i = 0; i < gameLevelsData.Count; i++)
        {
            var currentLevelObj = levelObjs.Find($"{i + 1}");
            if (currentLevelObj == null) continue;

            //get the max stars for this level
            maxStars += gameLevelsData[i].maxStars + gameLevelsData[i].maxVoidStars;

            //create level data
            if (playerLevelsData == null)
            {
                currentLevelObj.GetComponent<levelSpawner>().levelData = CustomTypes.MergeTypes(gameLevelsData[i], new PlayerLevelData());
            }
            else if (playerLevelsData.Count - 1 < i)
            {
                currentLevelObj.GetComponent<levelSpawner>().levelData = CustomTypes.MergeTypes(gameLevelsData[i], new PlayerLevelData());
            }
            else
            {
                currentLevelObj.GetComponent<levelSpawner>().levelData = CustomTypes.MergeTypes(gameLevelsData[i], playerLevelsData[i]);
                
                //update satr progress
                foreach (bool b in playerLevelsData[i].obtainedStars)
                {
                    if (b)
                    {
                        collectedStars++;
                        currentLevelObj.GetComponent<levelSpawner>().obCStars++;
                    }
                }
                foreach (bool b in playerLevelsData[i].obtainedVoidStars)
                {
                    if (b)
                    {
                        collectedStars++;
                        currentLevelObj.GetComponent<levelSpawner>().obVStars++;
                    }
                }
            }

            //update maxTime and targetScore
            currentLevelObj.GetComponent<levelSpawner>().DetermineTimeAndScore();

        }
        UpdateUI();
    }

    public void OnLevelSelect(GameObject lvl)
    {
        if (currentSpawner == lvl)
        {
            LevelDeselect();
            return;
        }

        LevelDeselect();
        currentSpawner = lvl;
        Util.ExecuteAfterDelay(this, ShowInfoBox, .12f);
        
    }

    public void LevelDeselect()
    {
        currentSpawner = null;

        //animation
        DOTween.To(() => infoBox.GetComponent<UIScale>().relativeSize, x => infoBox.GetComponent<UIScale>().relativeSize = x, new Vector2(0f, .3f), .1f).SetEase(Ease.OutExpo).OnComplete(() => {
            infoBox.gameObject.SetActive(false);
        });
    
    }

    public void OnLevelStart()
    {
        currentSpawner.GetComponent<levelSpawner>().Spawn();
        currentSpawner.GetComponent<levelSpawner>().completed.AddListener(OnLevelComplete);

        //animation
        DOTween.To(() => infoBox.GetComponent<UIScale>().relativeSize, x => infoBox.GetComponent<UIScale>().relativeSize = x, new Vector2(0f, .3f), .1f).SetEase(Ease.OutExpo).OnComplete(() => {
            infoBox.gameObject.SetActive(false);
        });
    }

    public void OnLevelComplete()
    {
        currentLevel = currentSpawner.GetComponent<levelSpawner>().levelData.level;
        UpdateUI();
    }

    private void UpdateUI()
    {
        bar.transform.Find("chapterTitle").GetComponent<TextMeshProUGUI>().text = currentChapter;
        bar.transform.Find("progressBar").Find("progress").GetComponent<UIScale>().relativeSize = new Vector2((float)collectedStars / maxStars, 1f);
        bar.transform.Find("progressBar").Find("text").GetComponent<TextMeshProUGUI>().text = ((float)collectedStars / maxStars * 100).ToString("N1") + "%";
        Debug.Log($"{collectedStars}, {maxStars}, {currentLevel}");

        var levels = transform.Find(currentChapter);
        if (levels == null) return;
        var lines = levels.Find("Lines");
        for (int i = 1; i <= currentLevel; i++)
        {
            var level = levels.Find($"{i}");
            level.GetComponent<Image>().sprite = Resources.Load<Sprite>("Utilitary/LevelImg/level");
            if (lines == null || i < 2) continue;
            lines.Find($"{i - 1} --> {i}").GetComponent<UILine>().color = new Color(255 / 255f, 215 / 255f, 0f);
        }
    }

    private void ShowInfoBox()
    {
        var spawnerScript = currentSpawner.GetComponent<levelSpawner>();
        if (spawnerScript == null) return;
        if (currentLevel < spawnerScript.levelData.level - 1)
        {
            infoBox.transform.Find("canComplete").gameObject.SetActive(false);
            infoBox.transform.Find("notCanComplete").gameObject.SetActive(true);
        }
        else
        {
            //update ui
            infoBox.transform.Find("notCanComplete").gameObject.SetActive(false);
            infoBox.transform.Find("canComplete").gameObject.SetActive(true);

            //level text
            infoBox.transform.Find("canComplete").Find("level").GetComponent<TextMeshProUGUI>().text = "Level " + spawnerScript.levelData.level.ToString();

            //stars collected
            var cStars = infoBox.transform.Find("canComplete").Find("stars").Find("cStars").Find("txt").GetComponent<TextMeshProUGUI>();
            var vStars = infoBox.transform.Find("canComplete").Find("stars").Find("vStars").Find("txt").GetComponent<TextMeshProUGUI>();
            
            cStars.text = spawnerScript.obCStars.ToString() + "/" + spawnerScript.levelData.maxStars.ToString();

            if (spawnerScript.levelData.maxVoidStars > 0)
            {
                vStars.transform.parent.gameObject.SetActive(true);
                vStars.text = spawnerScript.obVStars.ToString() + "/" + spawnerScript.levelData.maxVoidStars.ToString();
            }
            else
            {
                vStars.transform.parent.gameObject.SetActive(false);
            }


            //difficulty
            float difficulty = spawnerScript.difficulty;
            var diffText = infoBox.transform.Find("canComplete").Find("difficulty");
            var color = new Color(53 / 255f, 197 / 255f, 50 / 255f);
            int j = 1;
            if (1f <= difficulty && difficulty < 2.25f)
            {
                color = new Color(203 / 255f, 166 / 255f, 82 / 255f);
                j = 2;
            }
            else if (2.25f <= difficulty)
            {
                color = new Color(212 / 255f, 61 / 255f, 61 / 255f);
                j = 3;
            }
            diffText.GetComponent<TextMeshProUGUI>().color = color;
            diffText.GetComponent<TextMeshProUGUI>().text = "Difficulty: " + difficulty.ToString("F2");
            for (int a = 1; a <= 3; a++)
            {
                if (a <= j)
                {
                    diffText.Find("bars").Find(a.ToString()).GetComponent<Image>().color = color;
                }
                else
                {

                    diffText.Find("bars").Find(a.ToString()).GetComponent<Image>().color = new Color(0, 0, 0, 166 / 255f);
                }
            }

            //time
            int time = (int)spawnerScript.maxTime;
            int min = time / 60;
            int sec = time % 60;
            infoBox.transform.Find("canComplete").Find("time").Find("text").GetComponent<TextMeshProUGUI>().text = $"Time: {min:D2}:{sec:D2}";

            //best time
            if (spawnerScript.levelData.bestTime > 0)
            {
                min = (int)spawnerScript.levelData.bestTime / 60;
                sec = (int)spawnerScript.levelData.bestTime % 60;
                infoBox.transform.Find("canComplete").Find("bestTime").Find("text").GetComponent<TextMeshProUGUI>().text = $"Best Time: {min:D2}:{sec:D2}";
            }
            else
            {
                infoBox.transform.Find("canComplete").Find("bestTime").Find("text").GetComponent<TextMeshProUGUI>().text = "Best Time: --:--";
            }
        }

        //position
        int direction = 1;
        float offset = 0;
        var spUIScale = currentSpawner.GetComponent<UIScale>();
        var infUIScale = infoBox.GetComponent<UIScale>();

        if ((spUIScale.relativePosition.x + spUIScale.relativeSize.x / 2f) * .9f + .135f * 1.3f > .5f)
        {
            direction = -1;
        }
        float y1 = spUIScale.relativePosition.y * .825f + .3f / 2f;
        float y2 = spUIScale.relativePosition.y * .825f - .3f / 2f;
        if (y1 > .8f)
        {
            offset = .8f - y1;
        }
        if (y2 < .025f)
        {
            offset = .025f - y2;
        }
        offset /= .3f;

        infUIScale.relativePosition = new Vector2((spUIScale.relativePosition.x + spUIScale.relativeSize.x / 2f * 1.2f * direction) * .9f + .05f, spUIScale.relativePosition.y * .825f);

        //pointer
        if (direction == 1)
        {
            infoBox.transform.Find("trig").GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
            infoBox.transform.Find("trig").GetComponent<UIScale>().relativePosition = new Vector2(0f, .5f - offset);

            infoBox.GetComponent<RectTransform>().pivot = new Vector2(-.2f, .5f - offset);
        }
        else
        {
            infoBox.transform.Find("trig").GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
            infoBox.transform.Find("trig").GetComponent<UIScale>().relativePosition = new Vector2(1f, .5f - offset);

            infoBox.GetComponent<RectTransform>().pivot = new Vector2(1.2f, .5f - offset);
        }

        //aniamtion
        infoBox.gameObject.SetActive(true);
        DOTween.To(() => infoBox.GetComponent<UIScale>().relativeSize, x => infoBox.GetComponent<UIScale>().relativeSize = x, new Vector2(.135f, .3f), .2f).SetEase(Ease.OutBounce);
    }

}
