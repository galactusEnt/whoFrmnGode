using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int stars = 0, voidStars = 0;
    public bool playedTutorial = false;

    // [mini-hgame name] - high score
    public SerializableDictionary<string, int> endlessHighScores = new SerializableDictionary<string, int>
    {
        { "endlessGrid", 0 },
    };

    // [chapter name] - { level data }
    public SerializableDictionary<string, NestedList<PlayerLevelData>> storyModeProgress = new SerializableDictionary<string, NestedList<PlayerLevelData>>
    {
        {"The Beginnings", new NestedList<PlayerLevelData>() },
    };
}
