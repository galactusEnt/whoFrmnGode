using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSpawner : MonoBehaviour
{
    //level properties
    public float difficulty = 1;
    public int targetScore = 10000;
    public float modifSize = 1, modifScore = 1, modifImgNr = 1, modifTime = 1;
    public int startRound = 0;

    [HideInInspector] public int obCStars = 0, obVStars = 0;
    [HideInInspector] public int bestTime = 0;
    [HideInInspector] public float maxTime = 5;

    public Dictionary<string, float> nLevelGameModes = new Dictionary<string, float>
    {
        {"grid", 1f}
    };

    [HideInInspector] public LevelData levelData;

    //ui
    public GameObject levelPrefab, dialoguePrefab;

    //events
    public UnityEvent completed = new UnityEvent();

    public void Spawn()
    {
        if (levelData.startDialogueId == -1 || levelData.seenStartDialogue)
        {
            StartLevel();
            return;
        }
        var currentDialogue = Instantiate(dialoguePrefab, GameObject.FindGameObjectWithTag("canvas").transform);
        currentDialogue.GetComponent<dialogueMaaster>().DisplayLevelStory(transform.parent.name, levelData.startDialogueId);
        currentDialogue.GetComponent<dialogueMaaster>().SequenceFinished.AddListener(() =>
        {
            levelData.seenStartDialogue = true;
            StartLevel();
        });
    }

    public void StartLevel()
    {
        GameObject lvl = Instantiate(levelPrefab, GameObject.FindGameObjectWithTag("canvas").transform);
        lvl.GetComponent<miniGame>().spawner = gameObject.GetComponent<levelSpawner>();

        ApplyModifiers(lvl.GetComponent<miniGame>());
        lvl.GetComponent<miniGame>().startGame();

        lvl.transform.Find("finished").Find("ok").GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log(levelData.level + " " + levelData.endDialogueId + " " + levelData.seenEndDialogue);
            if (levelData.endDialogueId == -1 || levelData.seenEndDialogue) return;
            Debug.Log("b");
            var currentDialogue = Instantiate(dialoguePrefab, GameObject.FindGameObjectWithTag("canvas").transform);
            currentDialogue.GetComponent<dialogueMaaster>().DisplayLevelStory(transform.parent.name, levelData.endDialogueId);
            currentDialogue.GetComponent<dialogueMaaster>().SequenceFinished.AddListener(() =>
            {
                levelData.seenEndDialogue = true;
            });
        });
    }

    public void ApplyModifiers(miniGame lvl)
    {
        lvl.sizeMulti *= modifSize;
        lvl.scoreMulti *= modifScore;
        lvl.imageNrMulti *= modifImgNr;

        lvl.difficulty = difficulty;
        lvl.levelData = levelData.Clone();
        lvl.nLevelGameModes = nLevelGameModes;
    }

    public void UpdateData(miniGame lvl)
    {
        var data = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>().playerData;
        //update lvl data
        var newData = lvl.levelData;
        if (levelData.bestTime <= 0)
        {
            levelData.bestTime = newData.bestTime;
        }
        else
        {
            levelData.bestTime = Mathf.Min(newData.bestTime, levelData.bestTime);
        }

        //increse capacity of lists if not enough
        while (levelData.obtainedStars.Count < levelData.maxStars)
        {
            levelData.obtainedStars.Add(false);
        }
        while (levelData.obtainedVoidStars.Count < levelData.maxStars)
        {
            levelData.obtainedVoidStars.Add(false);
        }

        while (newData.obtainedStars.Count < levelData.maxStars)
        {
            newData.obtainedStars.Add(false);
        }
        while (newData.obtainedVoidStars.Count < levelData.maxStars)
        {
            newData.obtainedVoidStars.Add(false);
        }

        for (int i = 0; i < levelData.maxStars; i++)
        {
            // +1 golden star ONLY if not obtained already
            
            if (!levelData.obtainedStars[i] && newData.obtainedStars[i])
            {
                levelData.obtainedStars[i] = true;
                obCStars++;
                data.stars++;
                transform.parent.parent.GetComponent<levelSelector>().collectedStars++;
            }
            // +1 void star ONLY if not obtained already
            if (levelData.maxVoidStars <= 0) continue;

            if (!levelData.obtainedVoidStars[i] && newData.obtainedVoidStars[i])
            {
                levelData.obtainedVoidStars[i] = true;
                obVStars++;
                data.voidStars++;
                transform.parent.parent.GetComponent<levelSelector>().collectedStars++;
            }
        }

        //update file
        if (!data.storyModeProgress.ContainsKey(transform.parent.name))
        {
            data.storyModeProgress.Add(transform.parent.name, new NestedList<PlayerLevelData>());
        }

        if (data.storyModeProgress[transform.parent.name].Count >= levelData.level)
        {
            data.storyModeProgress[transform.parent.name][levelData.level - 1] = CustomTypes.UnMergeTypes(levelData).Item2;
        }
        else
        {
            data.storyModeProgress[transform.parent.name].Add(CustomTypes.UnMergeTypes(levelData).Item2);
        }

        completed.Invoke();
    }

    public void DetermineTimeAndScore()
    {
        targetScore = (int)(500 * Mathf.Pow(1 + difficulty, difficulty) * (1 + levelData.level / 10f));
        maxTime = 20 + targetScore / Mathf.Pow(10, -1 + (1 - .3f * difficulty / 5) * Mathf.Log10(targetScore)) * (.5f + .1f * difficulty);
        maxTime *= modifTime;
    }

}
