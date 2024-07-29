using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class miniGame: MonoBehaviour
{
    //init Parameters
    public string type = "endlessGrid";
    public int startRound = 0;
    public float maxTime = 5;

    //modifiers
    public float difficulty = 0;
    public float sizeMulti = 1, scoreMulti = 1, imageNrMulti = 1, timeMulti = 1;
    public int resetTimerMulti = 1;
    public Sprite[] sprites;

    //components
    public GameObject gameTable, timer, scoreboard;
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private AudioClip gameOverSound, finishSound, countSound, countGoSound;

    //playtime variables
    [HideInInspector] public int round = 1;
    [HideInInspector] public float score = 0;
    [HideInInspector] public bool finished = false;
    [HideInInspector] public GameObject currentQuest;

    //private variables
    private int timerResTimes = 1, timerResCount = 0;
    private bool playedCountdown = false;

    //events
    public UnityEvent gameEnded = new UnityEvent();

    #region main functions

    //go to menu when escape pressed
    private void Update()
    {
        //hide if game finished
        if (Input.GetKeyDown(KeyCode.Escape) && !finished)
        {
            pauseGame();
        }
    }
    public void pauseGame()
    {
        bool b = false;
        if (!transform.Find("inGameMenu").gameObject.activeSelf)
        {
            b = true;
        }
        transform.Find("inGameMenu").gameObject.SetActive(b);
        if (currentQuest != null)
        {
            currentQuest.transform.Find("questTimeBar").GetComponent<timer>().pauseTimer(b);
        }
        else
        {
            timer.GetComponent<timer>().pauseTimer(b);
        }
    }

    public void startGame()
    {
        if (!playedCountdown)
        {
            AnimateCountdown();
            return;
        }

        switch (type)
        {
            case "endlessGrid":
                endlessGrid_startGame();
                break;
            case "normalLevel":
                normalLevel_startGame();
                break;
            default:
                endlessGrid_startGame();
                break;
        }
    }

    public void showRound()
    {
        switch (type)
        {
            case "endlessGrid":
                endlessGrid_showRound();
                break;
            case "normalLevel":
                normalLevel_showRound();
                break;
            default:
                endlessGrid_showRound();
                break;
        }
    }

    public void nextRound()
    {
        switch (type)
        {
            case "endlessGrid":
                endlessGrid_nextRound();
                break;
            case "normalLevel":
                normalLevel_nextRound();
                break;
            default:
                endlessGrid_nextRound();
                break;
        }
    }

    public void restartGame()
    {
        switch (type)
        {
            case "endlessGrid":
                endlessGrid_restartGame();
                break;
            case "normalLevel":
                normalLevel_restartGame();
                break;
            default:
                endlessGrid_restartGame();
                break;
        }
    }

    public void endGame()
    {
        switch (type)
        {
            case "endlessGrid":
                endlessGrid_endGame();
                break;
            case "normalLevel":
                normalLevel_endGame();
                break;
            default:
                endlessGrid_endGame();
                break;
        }
    }

    public void destroy()
    {
        GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().ShowPanel();
        Destroy(gameObject);
    }
    #endregion

    #region game modes generator
    private void generator(string type)
    {
        switch (type)
        {
            case "grid":
                //calculate size
                int size = (int)Mathf.Min(20 * (.5f + difficulty / 10f), sizeMulti*(Mathf.Pow(round, 5f / (15 - difficulty)) + 2 + 1.5f*difficulty));

                //calculate impostors
                int nrOfTargets = (int)Mathf.Min(.4f * Mathf.Pow(size, 2), imageNrMulti * .01f * difficulty * round + 1);

                //parse properties to gameTable
                gameTable game = gameTable.GetComponent<gameTable>();
                game.targetCount = nrOfTargets;
                game.size = size;
                game.spriteTable = sprites;
                game.type = "grid";

                game.generate();

                break;
            case "question":
                var questObj = Instantiate(questionPrefab, transform.Find("questContainer"));
                currentQuest = questObj;
                questionMain questHandler = questObj.GetComponent<questionMain>();
                questHandler.nQuest = questNr;
                questHandler.difficulty = (.5f + .1f * Mathf.Pow(difficulty, 2)) * (1 + .2f * spawner.obCStars / spawner.levelData.maxStars);

                //handle events
                questHandler.onSolve.AddListener(normalLevel_onQuestSolve);
                questHandler.onFail.AddListener(normalLevel_onQuestFail);

                questObj.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    #endregion

    #region endlessGrid
    private void endlessGrid_startGame()
    {
        sprites = Resources.LoadAll<Sprite>("FriendMANs");

        round = 1; score = 0;
        scoreboard.GetComponent<scoreboard>().round = round;
        scoreboard.GetComponent<scoreboard>().score = (int)score;
        var data = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().playerData;
        scoreboard.GetComponent<scoreboard>().highScore = data.endlessHighScores["endlessGrid"];

        timer.gameObject.SetActive(true);
        timer.GetComponent<timer>().maxTime = maxTime;

        showRound();

        timer.GetComponent<timer>().restartTimer();
    }

    private void endlessGrid_showRound()
    {
        //generate grid
        generator("grid");

        //calculate round time
        timer.GetComponent<timer>().maxTime = timeMulti*Mathf.Max(3, maxTime - Mathf.Log10(startRound + round + 1) / Mathf.Log10(2) * (.1f + .1f * difficulty) - difficulty / 7f);

        timerResTimes = (int)(resetTimerMulti * (1 + (startRound + round) * (1 + .5f * difficulty) / 150f));
        timerResCount += 1;
        if (timerResCount == timerResTimes)
        {
            timer.GetComponent<timer>().restartTimer();
            timerResCount = 0;
        }
    }

    private void endlessGrid_nextRound()
    {
        //reward score
        score += Mathf.Min(10000000, 1 + scoreMulti * Mathf.Pow(1.05f, (startRound + round) *.8f * timer.GetComponent<timer>().currentTime / timer.GetComponent<timer>().maxTime) * (1 + difficulty));

        //advance round
        round += 1;

        scoreboard.GetComponent<scoreboard>().score = (int)score;
        scoreboard.GetComponent<scoreboard>().round = round;

        showRound();
    }

    private void endlessGrid_restartGame()
    {
        //hide "game over" screen
        transform.Find("gameOverImg").gameObject.SetActive(false);
        transform.Find("retry").gameObject.SetActive(false);
        transform.Find("giveUp").gameObject.SetActive(false);

        playedCountdown = false;
        finished = false;

        startGame();
    }

    private void endlessGrid_endGame()
    {
        timer.GetComponent<timer>().stopTimer();
        gameTable.GetComponent<gameTable>().clear();

        finished = true;
        gameEnded.Invoke();

        var data = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().playerData;
        if (score > data.endlessHighScores["endlessGrid"])
        {
            data.endlessHighScores["endlessGrid"] = (int)score;
        }

        AnimateGameOver();
    }
    #endregion

    #region normalLevel

    public Dictionary<string, float> nLevelGameModes = new Dictionary<string, float>
    {
        {"grid", 1f}
    };

    public int targetScore = 10000;
    public LevelData levelData;
    public levelSpawner spawner;

    private int currentStarId = 1, questNr = 1, starsCollected = 0;

    private void normalLevel_startGame()
    {
        sprites = Resources.LoadAll<Sprite>("friendMans");

        //calculate target score and time
        targetScore = spawner.targetScore;
        maxTime = spawner.maxTime;

        //reset parameters
        round = 1; score = 0;
        starsCollected = 0;

        //setup components

        //--scoreboard
        scoreboard.GetComponent<scoreboard>().score = (int)score;
        scoreboard.GetComponent<scoreboard>().level = spawner.levelData.level;
        scoreboard.GetComponent<scoreboard>().maxScore = targetScore;
        scoreboard.GetComponent<scoreboard>().updateStar(levelData.starPos);

        //--timer
        timer.gameObject.SetActive(true);
        timer.GetComponent<timer>().maxTime = maxTime;

        showRound();

        //start timer
        timer.GetComponent<timer>().restartTimer();
    }

    private void normalLevel_showRound()
    {
        //detect if end of level
        if (score / targetScore >= 1)
        {
            endGame();
            return;
        }

        //choose gamemode
        string gameMode = "grid";

        if (nLevelGameModes.Count > 1)
        {
            float multiplier = 1f;

            //function to generate a string of zeros
            string Zeros(int amount)
            {
                return "1" + new string('0', amount);
            }

            //adjust the multiplier
            foreach (var chance in nLevelGameModes)
            {
                string[] split = chance.Value.ToString().Split('.');
                if (split.Length > 1)
                {
                    int length = split[1].Length;
                    if (Convert.ToInt32(Zeros(length)) > multiplier)
                    {
                        multiplier = Convert.ToInt32(Zeros(length));
                    }
                }
            }

            float totalWeight = multiplier;

            System.Random random = new System.Random();
            float randomChance = (float)random.NextDouble() * totalWeight;
            float counter = 0f;

            foreach (var rarity in nLevelGameModes)
            {
                counter += rarity.Value;
                if (randomChance <= counter)
                {
                    gameMode = rarity.Key;
                    break;
                }
            }
        }

        //generate game mode specific table
        generator(gameMode);

    }

    private void normalLevel_nextRound()
    {
        //reward score
        score += targetScore / ((.7f + .2f * difficulty / 5f) * maxTime) * (1 - (1 - timer.GetComponent<timer>().currentTime / timer.GetComponent<timer>().maxTime) / 3f);
        //advance round
        round += 1;

        scoreboard.GetComponent<scoreboard>().score = (int)score;
        scoreboard.GetComponent<scoreboard>().round = round;

        if (normalLevel_testForQuest())
        {
            return;
        }

        //no question; next grid
        showRound();

    }

    private bool normalLevel_testForQuest()
    {
        float prog = score / targetScore;
        for (int i = starsCollected + 1; i <= levelData.maxStars; i++)
        {
            if (prog > levelData.starPos[i - 1])
            {
                starsCollected++;
                //--cStar ui
                scoreboard.GetComponent<scoreboard>().updateStar(i, 1);
                currentStarId = i; //save current star to update if question is solved

                if (levelData.obtainedStars.Count < i)
                {
                    levelData.obtainedStars.Add(true);
                }
                else
                {
                    levelData.obtainedStars[i - 1] = true;
                }

                if (levelData.obtainedVoidStars.Count < i)
                {
                    levelData.obtainedVoidStars.Add(false);
                }
                else
                {
                    levelData.obtainedVoidStars[i - 1] = false;
                }

                //detect if quest needed
                if (levelData.starQ.Count <= 0) break;

                questNr = levelData.starQ[i - 1];
                if (questNr <= 0) break;

                //need to show question
                timer.GetComponent<timer>().stopTimer();

                //ui
                gameTable.SetActive(false);
                timer.SetActive(false);

                generator("question");
                return true;
            }
        }
        return false;
    }

    private void normalLevel_onQuestSolve()
    {
        //--star ui
        scoreboard.GetComponent<scoreboard>().updateStar(currentStarId, 2);

        if (levelData.obtainedVoidStars.Count < currentStarId)
        {
            levelData.obtainedVoidStars.Add(true);
        }
        else
        {
            levelData.obtainedVoidStars[currentStarId - 1] = true;
        }

        if (normalLevel_testForQuest())
        {
            return;
        }
        //detect if end of level
        if (score / targetScore >= 1)
        {
            endGame();
            return;
        }

        //ui
        gameTable.SetActive(true);
        timer.SetActive(true);

        showRound();

        //bonus for answering
        timer.GetComponent<timer>().currentTime *= 1.1f;
        timer.GetComponent<timer>().tweenBar(true);

        Util.ExecuteAfterDelay(this, () =>
        {
            //start timer
            timer.GetComponent<timer>().startTimer();
        }, .5f);

    }
    private void normalLevel_onQuestFail()
    {
        if (normalLevel_testForQuest())
        {
            return;
        }
        //detect if end of level
        if (score / targetScore >= 1)
        {
            endGame();
            return;
        }

        //ui
        gameTable.SetActive(true);
        timer.SetActive(true);

        showRound();

        //penalisation for not answering
        timer.GetComponent<timer>().currentTime *= .9f;
        timer.GetComponent<timer>().tweenBar(false);

        Util.ExecuteAfterDelay(this, () =>
        {
            //start timer
            timer.GetComponent<timer>().startTimer();
        }, .5f);
    }

    private void normalLevel_restartGame()
    {
        //hide "game over" screen
        transform.Find("gameOverImg").gameObject.SetActive(false);
        transform.Find("retry").gameObject.SetActive(false);
        transform.Find("giveUp").gameObject.SetActive(false);

        //destroy possible question and dialogue
        if (currentQuest != null) Destroy(currentQuest);

        //update stars
        for (int i = 1; i <= Mathf.Min(levelData.maxStars, levelData.obtainedStars.Count); i++)
        {
            levelData.obtainedStars[i - 1] = false;
            levelData.obtainedVoidStars[i - 1] = false;
            scoreboard.GetComponent<scoreboard>().updateStar(i, 0);
        }

        playedCountdown = false;
        finished = false;

        startGame();
    }

    private void normalLevel_endGame()
    {
        timer.GetComponent<timer>().stopTimer();
        gameTable.GetComponent<gameTable>().clear();
        Destroy(currentQuest);

        finished = true;
        gameEnded.Invoke();

        //check if a star was obtaiend
        if (levelData.obtainedStars.Count > 0 && levelData.maxVoidStars == 0)
        {
            if (levelData.obtainedStars[0])
            {
                AnimateFinish();
                return;
            }
        } else if (levelData.obtainedStars.Count > 0 && levelData.maxVoidStars > 0)
        {
            if (levelData.obtainedStars[0] && levelData.obtainedVoidStars[0])
            {
                AnimateFinish();
                return;
            }
        }
        
        //ran out of time and no star
        AnimateGameOver();
    }
    #endregion

    #region Animations

    public void AnimateCountdown()
    {
        var countdown = transform.Find("countdown");
        countdown.DOKill();
        var scale = countdown.GetComponent<UIScale>();
        var rect = countdown.GetComponent<RectTransform>();

        //reset
        countdown.GetComponent<TextMeshProUGUI>().text = "3";
        countdown.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);

        countdown.gameObject.SetActive(true);
        gameTable.SetActive(false);
        scoreboard.SetActive(false);
        timer.SetActive(false);

        var sequence = DOTween.Sequence(countdown);
        sequence.Append(DOTween.To(() => scale.relativeSize, x => scale.relativeSize = x, new Vector2(.2f, .2f), .5f).SetEase(Ease.InBack).OnComplete(() => { GetComponent<AudioSource>().PlayOneShot(countSound); }));
        sequence.AppendInterval(.5f);
        sequence.Append(countdown.DORotate(new Vector3(0, 0, -360), .5f, RotateMode.WorldAxisAdd).SetEase(Ease.InBack).OnComplete(() => { countdown.GetComponent<TextMeshProUGUI>().text = "2"; GetComponent<AudioSource>().PlayOneShot(countSound); }));
        sequence.AppendInterval(.5f);
        sequence.Append(countdown.DORotate(new Vector3(0, 0, -360), .5f, RotateMode.WorldAxisAdd).SetEase(Ease.InBack).OnComplete(() => { countdown.GetComponent<TextMeshProUGUI>().text = "1"; GetComponent<AudioSource>().PlayOneShot(countSound); }));
        sequence.AppendInterval(.5f);
        sequence.Append(countdown.DORotate(new Vector3(0, 0, -360), .5f, RotateMode.WorldAxisAdd).SetEase(Ease.InBack).OnComplete(() => { countdown.GetComponent<TextMeshProUGUI>().text = "GO!"; GetComponent<AudioSource>().PlayOneShot(countGoSound); }));
        sequence.AppendInterval(.5f);
        sequence.Append(DOTween.To(() => scale.relativeSize, x => scale.relativeSize = x, new Vector2(0, .2f), .5f).SetEase(Ease.OutBack));
        sequence.AppendCallback(() =>
        {
            playedCountdown = true;
            countdown.gameObject.SetActive(false);

            gameTable.SetActive(true);
            scoreboard.SetActive(true);
            timer.SetActive(true);

            startGame();
        });
    }

    public void AnimateGameOver()
    {
        if (spawner != null)
        {
            spawner.UpdateData(this);
        }

        //play sound
        GetComponent<AudioSource>().PlayOneShot(gameOverSound);

        //tween IN "game over" text and buttons
        var gameOver = transform.Find("gameOverImg");
        gameOver.GetComponent<UIScale>().relativeSize = new Vector2(2, 2);
        gameOver.gameObject.SetActive(true);
        DOTween.To(() => gameOver.GetComponent<UIScale>().relativeSize, x => gameOver.GetComponent<UIScale>().relativeSize = x, new Vector2(.6f, .6f), .5f).SetEase(Ease.OutExpo);
        transform.Find("retry").gameObject.SetActive(true);
        transform.Find("giveUp").gameObject.SetActive(true);
    }

    public void AnimateFinish()
    {
        levelData.bestTime = MathF.Max(levelData.bestTime, maxTime - timer.GetComponent<timer>().currentTime);

        if (spawner != null)
        {
            spawner.UpdateData(this);
        }

        //play sound
        GetComponent<AudioSource>().PlayOneShot(finishSound);

        //star was collcted; completed screen
        var finished = transform.Find("finished");
        gameTable.SetActive(false);
        timer.SetActive(false);
        scoreboard.SetActive(false);
        transform.Find("hamMenu").gameObject.SetActive(false);
        finished.gameObject.SetActive(true);

        var finishTxt = finished.Find("text");
        DOTween.To(() => finishTxt.GetComponent<UIScale>().relativeSize, x => finishTxt.GetComponent<UIScale>().relativeSize = x, new Vector2(.5f, .2f), .3f).SetEase(Ease.InBack);
        Util.ExecuteAfterDelay(this, () =>
        {
            Util.EmitParticles(this, finishTxt.Find("particles").GetComponent<ParticleSystem>(), (int)(8 * PlayerPrefs.GetInt("Particles", 3) / 3f));
        }, .2f);

        var levelTxt = finished.Find("level");
        levelTxt.GetComponent<TextMeshProUGUI>().text = spawner.transform.parent.name + ": Level " + levelData.level.ToString();

        var time = finished.Find("time");
        int min = (int)levelData.bestTime / 60;
        int sec = (int)levelData.bestTime % 60;
        time.Find("txt").GetComponent<TextMeshProUGUI>().text = $"In {min:D2}:{sec:D2}";

        var stars = finished.Find("stars");
        if (levelData.maxVoidStars > 0)
        {
            stars.Find("vStars").gameObject.SetActive(true);
        }
        stars.Find("cStars").Find("txt").GetComponent<TextMeshProUGUI>().text = $"{spawner.obCStars}/{levelData.maxStars}";
        stars.Find("vStars").Find("txt").GetComponent<TextMeshProUGUI>().text = $"{spawner.obVStars}/{levelData.maxVoidStars}";

        var ok = finished.Find("ok");

        Util.ExecuteAfterDelay(this, () =>
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => finishTxt.GetComponent<UIScale>().relativePosition, x => finishTxt.GetComponent<UIScale>().relativePosition = x, new Vector2(.5f, .7f), .3f));
            seq.Join(levelTxt.GetComponent<TextMeshProUGUI>().DOColor(new Color(0, 0, 0, 210 / 255f), .3f));
            seq.Append(DOTween.To(() => time.GetComponent<UIScale>().relativePosition, x => time.GetComponent<UIScale>().relativePosition = x, new Vector2(.5f, .45f), .1f).SetEase(Ease.OutExpo));
            seq.Append(DOTween.To(() => stars.GetComponent<UIScale>().relativePosition, x => stars.GetComponent<UIScale>().relativePosition = x, new Vector2(.5f, .35f), .1f).SetEase(Ease.OutExpo));
            seq.Append(DOTween.To(() => ok.GetComponent<UIScale>().relativePosition, x => ok.GetComponent<UIScale>().relativePosition = x, new Vector2(.5f, .15f), .1f).SetEase(Ease.OutExpo));
        }, 1.2f);
    }

    #endregion
}
