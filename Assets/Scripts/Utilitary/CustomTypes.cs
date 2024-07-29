using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CustomTypes
{
    public static LevelData MergeTypes(GameLevelData gameLevelData, PlayerLevelData playerLevelData)
    {
        LevelData levelData = new LevelData();

        levelData.level = gameLevelData.level;
        levelData.maxStars = gameLevelData.maxStars;
        levelData.maxVoidStars = gameLevelData.maxVoidStars;
        levelData.starPos = new List<float>(gameLevelData.starPos);
        levelData.starQ = new List<int>(gameLevelData.starQ);
        levelData.startDialogueId = gameLevelData.startDialogueId;
        levelData.endDialogueId = gameLevelData.endDialogueId;

        levelData.bestTime = playerLevelData.bestTime;
        levelData.obtainedStars = new List<bool>(playerLevelData.obtainedStars);
        levelData.obtainedVoidStars = new List<bool>(playerLevelData.obtainedVoidStars);
        levelData.seenStartDialogue = playerLevelData.seenStartDialogue;
        levelData.seenEndDialogue = playerLevelData.seenEndDialogue;

        return levelData;
    }
    public static (GameLevelData, PlayerLevelData) UnMergeTypes(LevelData levelData)
    {
        GameLevelData gameLevelData = new GameLevelData();
        PlayerLevelData playerLevelData = new PlayerLevelData();

        gameLevelData.level = levelData.level;
        gameLevelData.maxStars = levelData.maxStars;
        gameLevelData.maxVoidStars = levelData.maxVoidStars;
        gameLevelData.starPos = new List<float>(levelData.starPos);
        gameLevelData.starQ = new List<int>(levelData.starQ);
        gameLevelData.startDialogueId = levelData.startDialogueId;
        gameLevelData.endDialogueId = levelData.endDialogueId;

        playerLevelData.bestTime = levelData.bestTime;
        playerLevelData.obtainedStars = new List<bool>(levelData.obtainedStars);
        playerLevelData.obtainedVoidStars = new List<bool>(levelData.obtainedVoidStars);
        playerLevelData.seenStartDialogue = levelData.seenStartDialogue;
        playerLevelData.seenEndDialogue = levelData.seenEndDialogue;

        return (gameLevelData, playerLevelData);
    }
}

//Util
#region Util

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new();
    [SerializeField]
    private List<TValue> values = new();

    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != values.Count)
        {
            throw new Exception("Keys and Values number do not match!" + keys.Count + " != " + values.Count);
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this[keys[i]] = values[i];
        }
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach(var pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
}

[Serializable] public class NestedList<T>: List<T>, ISerializationCallbackReceiver
{
    public List<T> items = new();

    public void OnAfterDeserialize()
    {
        Clear();
        for (int i = 0; i<items.Count; i++)
        {
            Add(items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        items.Clear();
        for (int i = 0; i < Count; i++)
        {
            items.Add(this[i]);
        }
    }
}
#endregion

//Level Data
#region Level Data
[Serializable]
public class GameLevelData
{
    public int level = 1, maxStars = 0, maxVoidStars = 0;
    public List<float> starPos = new List<float>();
    public List<int> starQ = new List<int>();
    public int startDialogueId = -1;
    public int endDialogueId = -1;
}

[Serializable]
public class PlayerLevelData
{
    public float bestTime = -1;
    public List<bool> obtainedStars = new List<bool>();
    public List<bool> obtainedVoidStars = new List<bool>();
    public bool seenStartDialogue = false;
    public bool seenEndDialogue = false;
}

[Serializable]
public class LevelData
{
    public int level = 1, maxStars = 0, maxVoidStars = 0;
    public List<float> starPos = new List<float>();
    public List<int> starQ = new List<int>();
    public int startDialogueId = -1;
    public int endDialogueId = -1;

    public float bestTime = 0;
    public List<bool> obtainedStars = new List<bool>();
    public List<bool> obtainedVoidStars = new List<bool>();
    public bool seenStartDialogue = false;
    public bool seenEndDialogue = false;

    public LevelData Clone()
    {
        LevelData levelData = new LevelData();
        levelData.level = level;
        levelData.maxStars = maxStars;
        levelData.maxVoidStars = maxVoidStars;
        levelData.starPos = new List<float>(starPos);
        levelData.starQ = new List<int>(starQ);
        levelData.startDialogueId = startDialogueId;
        levelData.endDialogueId = endDialogueId;

        levelData.bestTime = bestTime;
        levelData.obtainedStars = new List<bool>(obtainedStars);
        levelData.obtainedVoidStars = new List<bool>(obtainedVoidStars);
        levelData.seenStartDialogue = seenStartDialogue;
        levelData.seenEndDialogue = seenEndDialogue;
        return levelData;
    }
}
#endregion

//Dialogue Data
#region Dialogue Data
[Serializable]
public class DialogueData
{
    public bool destroyPreviousCharacters = false;
    public bool destroyPreviousPicture = false;
    public List<string> characters = new List<string>();
    public List<bool> isOnRight = new List<bool>();
    public string text = "FirendMAN is THE BEST! -G";
    public string pathToBackground = ""; //from Assets/Resources
    public string pathToPicture = ""; //from Assets/Resources
}

[Serializable]
public class StorySegment
{
    public List<DialogueData> sequence = new List<DialogueData>();
    public bool canBeNavigated = false;
}
#endregion

//Mini-game Data
#region Mini-game Data
[Serializable]
public class MiniGameSettings
{
    public float
        Difficulty = 0,
        MaxTime = 8,
        SizeMulti = 1,
        ScoreMulti = 1,
        ImageNrMulti = 1,
        TimeMulti = 1,
        ResetTimerMulti = 1;

    public float GetProperty(string key)
    {
        switch (key)
        {
            case "Difficulty": return Difficulty;
            case "MaxTime": return MaxTime;
            case "SizeMulti": return SizeMulti;
            case "ScoreMulti": return ScoreMulti;
            case "ImageNrMulti": return ImageNrMulti;
            case "TimeMulti": return TimeMulti;
            case "ResetTimerMulti": return ResetTimerMulti;
            default: return 0;
        }
    }
}
#endregion