using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public SerializableDictionary<string, NestedList<MiniGameSettings>> endlessSettings = new SerializableDictionary<string, NestedList<MiniGameSettings>>
    {
        {
            "grid", new NestedList<MiniGameSettings>
            {
                new MiniGameSettings
                {
                    Difficulty = 0f,
                    MaxTime = 8f,
                    SizeMulti = .9f,
                    ScoreMulti = .8f,
                    ImageNrMulti = .5f,
                    TimeMulti = 1.2f,
                    ResetTimerMulti = 1
                },
                new MiniGameSettings
                {
                    Difficulty = 1.5f,
                    MaxTime = 5f,
                    SizeMulti = 1f,
                    ScoreMulti = 1f,
                    ImageNrMulti = 1f,
                    TimeMulti = 1f,
                    ResetTimerMulti = 2
                },
                new MiniGameSettings
                {
                    Difficulty = 2f,
                    MaxTime = 6f,
                    SizeMulti = 1.2f,
                    ScoreMulti = 1.5f,
                    ImageNrMulti = 1.3f,
                    TimeMulti = .9f,
                    ResetTimerMulti = 2
                },
                new MiniGameSettings
                {
                    Difficulty = 2.75f,
                    MaxTime = 8,
                    SizeMulti = 1.4f,
                    ScoreMulti = 2f,
                    ImageNrMulti = 1.8f,
                    TimeMulti = .9f,
                    ResetTimerMulti = 2
                },
            }
        },
    };

    public SerializableDictionary<string, NestedList<GameLevelData>> storyMode = new SerializableDictionary<string, NestedList<GameLevelData>>
    {
        {
            "The Beginnings", new NestedList<GameLevelData>
            {
                new GameLevelData
                {
                    level = 1,
                    maxStars = 1,
                    maxVoidStars = 1,
                    starPos = {1},
                    starQ = {1},
                    startDialogueId = 0,
                },
                new GameLevelData
                {
                    level = 2,
                    maxStars = 1,
                    maxVoidStars = 0,
                    starPos = {1f},
                },
                new GameLevelData
                {
                    level = 3,
                    maxStars = 1,
                    maxVoidStars = 1,
                    starPos = {1f},
                    starQ = {1},
                    startDialogueId = 1,
                    endDialogueId = 3,
                },
                new GameLevelData
                {
                    level = 4,
                    maxStars = 1,
                    maxVoidStars = 1,
                    starPos = {1f},
                    starQ = {2},
                },
                new GameLevelData
                {
                    level = 5,
                    maxStars = 2,
                    maxVoidStars = 2,
                    starPos = {.5f, 1f},
                    starQ = {1, 1},
                    endDialogueId = 4,
                },
                new GameLevelData
                {
                    level = 6,
                    maxStars = 3,
                    maxVoidStars = 3,
                    starPos = {.6f, .8f, 1f},
                    starQ = {2, 3, 5},
                    startDialogueId = 5,
                    endDialogueId = 6,
                },
                new GameLevelData
                {
                    level = 7,
                    maxStars = 2,
                    maxVoidStars = 1,
                    starPos = {.4f, 1f},
                    starQ = {1, 1},
                },
                new GameLevelData
                {
                    level = 8,
                    maxStars = 2,
                    maxVoidStars = 2,
                    starPos = {.5f, 1f},
                    starQ = {1, 2},
                    startDialogueId = 7,
                },
                new GameLevelData
                {
                    level = 9,
                    maxStars = 3,
                    maxVoidStars = 2,
                    starPos = {.4f, .7f, 1f},
                    starQ = {0, 2, 2},
                    startDialogueId = 8,
                },
                new GameLevelData
                {
                    level = 10,
                    maxStars = 3,
                    maxVoidStars = 3,
                    starPos = {.4f, .7f, 1f},
                    starQ = {1, 2, 3},
                },
                new GameLevelData
                {
                    level = 11,
                    maxStars = 4,
                    maxVoidStars = 3,
                    starPos = {.1f, .4f, .7f, 1f},
                    starQ = {2, 0, 3, 4},
                    startDialogueId = 9,
                    endDialogueId = 10,
                },
                new GameLevelData
                {
                    level = 12,
                    maxStars = 3,
                    maxVoidStars = 2,
                    starPos = {.2f, .4f, 1f},
                    starQ = {0, 2, 3},
                    startDialogueId = 11,
                },
                new GameLevelData
                {
                    level = 13,
                    maxStars = 3,
                    maxVoidStars = 3,
                    starPos = {.25f, .65f, 1f},
                    starQ = {2, 4, 6},
                    startDialogueId = 12,
                    endDialogueId = 13,
                },
                new GameLevelData
                {
                    level = 14,
                    maxStars = 4,
                    maxVoidStars = 4,
                    starPos = {.25f, .5f, .75f, 1f},
                    starQ = {3, 2, 5, 6},
                    endDialogueId = 14,
                },
                new GameLevelData
                {
                    level = 15,
                    maxStars = 5,
                    maxVoidStars = 5,
                    starPos = {.4f, .55f, .7f, .85f, 1f},
                    starQ = {5, 5, 5, 7, 10},
                    startDialogueId = 15,
                },
            }
        },
    };

    public SerializableDictionary<string, NestedList<StorySegment>> dialogueStoryMode = new SerializableDictionary<string, NestedList<StorySegment>>
    {
        {
            "Tutorial", new NestedList<StorySegment>
            {
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "Hello, Player! Welcome to the MOST amazing game, \"Who's That FriendMAN?\"!",
                            pathToPicture = "Tutorial/title",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "You're probably asking yourself who FriendMAN is... The thing is, I don't know either.",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "The developer didn't add the introduction part of the story yet... :(",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "But you're not here to learn about that, are you? " +
                            "You're here to learn HOW TO PLAY! Yeah, that's why you're here.",
                            pathToPicture = "Tutorial/howToPlay",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "Well, I feel that I have to begin with saying that this game is not that simple. " +
                            "It tests your reaction time, visual memory, critical thinking aaand... Maths Skills!",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "If you're not into that then you shuld play some something else instead...",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "Now, let's get to it! You'll see, it's quite fun!",
                            pathToPicture = "",
                        },
                    }
                },
                //The GRID Game + question; to be separated when making a tutorial menu
                new StorySegment
                {
                    canBeNavigated = true,
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "I will explain now [The GRID Game]! " +
                            "A fun mini-game designed by VOID to test your reaction time and vision.",
                            pathToPicture = "Tutorial/theGridGame",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "This is the game table. As you can see, it is filled with identical FriendMANs (whatever those are).",
                            pathToPicture = "Tutorial/gridAnno",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "What you need to do is find the slightly <altered> FriendMAN and click on it. " +
                            "Pay attention to the timer at the bottom of the screen! If it runs out, the game is OVER!",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "By doing this, you receive points, reset the timer (if not in a level) " +
                            "and progress to the next round.",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "You can find this mini-game in [Endless Mode] as well as in [Story Mode]. " +
                            "Talking about the Story Mode...",
                            pathToPicture = "Tutorial/menuAnno",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "I have to explain to you QUESTIONS! " +
                            "VOID's intricate design to really make you fail every level!",
                            pathToPicture = "Tutorial/quTime",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "Now, questions are not that hard... As a game mechanic at least. " +
                            "You are presented with a \"little\" (or not) equation that you need to solve!",
                            pathToPicture = "Tutorial/multich",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "You either need to choose the correct answer...",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "...or, if on a higher difficulty, type it by hand! (that's diabolical I think)",
                            pathToPicture = "Tutorial/textwrit",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "Whatever the answer method, you need think and calculate fast because the question is also time limited.",
                            pathToPicture = "",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "I think those were the basics and now you know how to play this <AMAZING!> game. Or...",
                            pathToPicture = "Tutorial/title",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "One more thing, the levels are controlled by a difficulty parameter that you can see in the [Info Box], " +
                            "along with the level time, [Golden Stars] and [VOID Stars] available and your Best Time in completing the level.",
                            pathToPicture = "Tutorial/infoBox",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            isOnRight = {true},
                            text =
                            "To complete a level, you need to gain 1 [Golden Star] and, if there are any, 1 [Void Star]!",
                            pathToPicture = "Tutorial/scoreb",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "Now I think I finished all that I had to say to a beginner. " +
                            "I hope we'll meet again... if the developer continues this game...",
                            pathToPicture = "Tutorial/title",
                        },
                        new DialogueData
                        {
                            characters = {"Starry The Star"},
                            text =
                            "I wish you good luck and have fun playing \"Who's That FriendMAN?\"!",
                            pathToPicture = "",
                        },
                    }
                }
            }
        },
        {
            "The Beginnings", new NestedList<StorySegment>
            {
                //0 - enter of chapter
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "We have arrived at the first stop of our long journey. " +
                            "These ruins are the remains of an ancient civilization and " +
                            "no one knows what happened to them. ",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "Whatever was the thing that attacked them, it must have been very deangerous! " +
                            "Their technology was very advanced for ancient times.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            isOnRight = {true},
                            text =
                            "We came here, because we need a key that will become useful later on. " +
                            "I am not sure where it is exactly, but it has to be somewhere here. " +
                            "Let’s start with this watchtower, perhaps we will find clues about where key is kept.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        }
                    }
                },
                //1 - tutorial que
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* Wisam stands before a very big gate full of ancient runes and many circuits and electronics *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //gate pic
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "It seems that we will have to prove that we are worthy of entering here. " +
                            "To do so we will have to use MATHEMATICS!",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //2 - tutorial finished; enter of first question level
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "I think you are now ready and got the knowledge to answer the Maths questions.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "You will need to defeat VOID’s mischief, by solving his puzzles. " +
                            "He knows we are coming after him and he will not make our journey any easier. ",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //3 - finished first question level
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* rumbling sound *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "Seems like you did it, Player! Your first question!" +
                            "I would like to congratulate you more, but we don’t have time for it right now.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            isOnRight = {true},
                            text =
                            "We need to explore this watchtower and uncover clues of where we could find The Key.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //4 - tower enter
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* you and Wisam enter the ancient watchtower. " +
                            "its stone walls covered in vines and mysterious symbols. " +
                            "the air is thick with a scent of moss and the faint sound of distant echoes. *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //watchtower pic
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "Stay alert, Player! VOID’s tricks are everywhere...",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* a very deep voice resonated in the whole ruin: " +
                            "\"I wouldn’t call myself a trick, but I would still advise you to be careful.\" *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "(worried) That sounded like a troll. He may know where we could find The Key.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //5 - meet Loll The Troll; enter troll battle
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* you and Wisam enter the room of the troll *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll"},
                            isOnRight = {true},
                            text =
                            "Hm... I didn’t expect any guests... " +
                            "Who are you and why you are in my home?!",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam the Wise", "Loll The Troll"},
                            isOnRight = {false, true},
                            text =
                            "We are just two travelers looking for The Key " +
                            "that was once guarded by the people of this kingdom.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam the Wise"},
                            isOnRight = {true, false},
                            text =
                            "You know they disappeared, trying to PROTECT it? " +
                            "Hmm... Perhaps... " +
                            "...you want to follow them?",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam the Wise", "Loll The Troll"},
                            isOnRight = {false, true},
                            text =
                            "No, that’s why I want YOU to help us. " +
                            "You know a thing or two about this place, don’t you?",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam the Wise"},
                            isOnRight = {true, false},
                            text =
                            "I may do that...",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam the Wise", "Loll The Troll"},
                            isOnRight = {false, true},
                            text =
                            "That's wonderf...",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam the Wise"},
                            isOnRight = {true, false},
                            text =
                            "...BUT only if you do what I want before.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam the Wise", "Loll The Troll"},
                            isOnRight = {false, true},
                            text =
                            "Oh... What would a Troll like you want?",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam the Wise"},
                            isOnRight = {true, false},
                            text =
                            "Would I be a Troll if I wouldn't ask you any riddles? " +
                            "(laughing) But not just any riddles, mathematical riddles",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },

                    }
                },
                //6 - finished troll battle
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* the Troll starts laughing histerically *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll"},
                            isOnRight = {true},
                            text =
                            "That was fun, right!? Now that you answered my questions, " +
                            "I will help you.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },

                    }
                },
                //7 - troll explains the ankh and sphynx
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Loll The Troll"},
                            text =
                            "The key that you want to get is being kept by The Sphynx.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll"},
                            isOnRight = {true, false},
                            text =
                            "The Sphynx? Isn’t this place supposed to be empty?",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {false, true},
                            text =
                            "Yes! But before the ancient civilization that lived here disappeared, " +
                            "they left someone, or something else to take care of their job.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {false, true},
                            text =
                            "So they made The Sphynx. " +
                            "The problem is that The Sphynx has high telekinetic giving it the ability to smash you into the walls.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {false, true},
                            text =
                            "The only way you can be protected from this power is if you wear The Ancient Ankh.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll"},
                            isOnRight = {true, false},
                            text =
                            "And where would someone find this Ancient Ankh?",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {false, true},
                            text =
                            "The good news for you are that I know where it is and I can bring you there! " +
                            "But the bad news are that I don’t know how to open the box in which The Ankh is being kept in.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {false, true},
                            text =
                            "(with a wide smile) I think you will need to use those mathematical powers again...",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll"},
                            isOnRight = {true, false},
                            text =
                            "Just get us there and we will handle the details.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },

                    }
                },
                //8 - journey to the box with ankh
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* you walk along Wisam behind The Troll. he seems to know where he is going, " +
                            "walking freely through the watchtower *",
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* you notice the weird black goo coming out of his chest. " +
                            "it looks like some sort of tentacle creature... *",
                            //pic with trolls void
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* at some point he finds some old and mossy stairs. " +
                            "after looking a little at them, he starts walking up. *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* the stairs felt endless, like they were going beyond the clouds and even further. " +
                            "after what felt like hours of walking, you arrived at the top of the tower. *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* you go to the nearest window and you see that you are in space... *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //pic with tower in sapce
                        },

                    }
                },
                //9 - box open level
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {true, false},
                            text =
                            "This is the box that you need to open.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //pic with the box
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll" },
                            isOnRight = {true, true},
                            text =
                            "* takes the box in his hands *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll" },
                            isOnRight = {false, true},
                            text =
                            "It looks like The Troll was right. Player, prepare your Maths skills!",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },

                    }
                },
                //10 - opeend box
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* the box opens and you can see The Ankh *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //pic with ankh in the box
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll" },
                            isOnRight = {false, true},
                            text =
                            "Good job! Now we won’t have to be worried about beign thrown around by a lion with the head of a human.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },

                    }
                },
                //11 - need for magic words
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {true, false},
                            text =
                            "There is one more thing that you should know before heading to The Sphynx. " +
                            "He is located inside the temple in the middle of the kingdom. " +
                            "To enter, you need to say some ~magic words~.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = { "Wisam The Wise", "Loll The Troll" },
                            isOnRight = {false, true},
                            text =
                            "What are the ~magic words~ ?",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {true, false },
                            text =
                            "The words are... Hm... It seems that I can't manage to remember! " +
                            "But we can go to The Library. There is a scroll that has the words written on it.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //12 - arrival at the library; enter scroll level
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* The Troll took you to an ancient library full of dusty scrolls *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {true, false },
                            text =
                            "This was The Old Library that they used to keep all of their scrolls. " +
                            "As you can see, it's a disaster... ",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //pic with library
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {true, false },
                            text =
                            "There are a lot of scrolls here. " +
                            "I recommend that you start from this corner and then...",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll"},
                            isOnRight = {false, true},
                            text =
                            "We don’t have time for this. Player I will need your help with a spell!" +
                            "If we succeed, the scroll should come to us!",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //13 - scroll obtained
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* the scroll flies into Wisam’s hand *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "Here you are! " +
                            "Let’s see what security protocol such an ancient and advanced society used.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "* opens the scroll: just say let me enter, please *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //pic with scroll
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "I guess cybersecurity was never their strong suit...",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //14 - troll disappointed; arrival at temple
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {true, false },
                            text =
                            "Oh... You found it... (sarcastic) Yaaay!..." +
                            "Let me guide you to the temple then.",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            destroyPreviousCharacters = true,
                            characters = {},
                            text =
                            "* The Temple stands before you. " +
                            "its stone structure is partially hidden by the thick foliage that " +
                            "has grown over centuries. *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //temple pic
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* vines drape over the massive stone blocks, and moss covers much of the exterior. *",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            destroyPreviousPicture = true,
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {false, true},
                            text =
                            "I won’t come with you any further. " +
                            "I wish you success! ",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Loll The Troll", "Wisam The Wise"},
                            isOnRight = {false, true},
                            text =
                            "(mumbling) ...or not",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise", "Loll The Troll"},
                            isOnRight = {true, false},
                            text =
                            "Thank you for everything! Take care!",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                //15 - entering the temple
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            text =
                            "Let me enter, please!!" ,
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            destroyPreviousCharacters = true,
                            characters = {},
                            text =
                            "* a grand hallway now leads to the entrance of a huge room, flanked by towering stone statues of guardians with glowing eyes. *" ,
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* as you approach, the ground beneath your feet hums faintly, and you notice thin, barely visible lines of light tracing patterns across the steps. *" ,
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                            //pic with floor
                        },
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "* the large stone doors, covered in ancient symbols, seem to respond to your presence, sliding open smoothly without a sound *" ,
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        new DialogueData
                        {
                            characters = {"Wisam The Wise"},
                            isOnRight = {true},
                            text =
                            "I hope you are ready because now there is no going back." ,
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                    }
                },
                new StorySegment
                {
                    sequence =
                    {
                        new DialogueData
                        {
                            characters = {},
                            text =
                            "",
                            pathToBackground = "Utilitary/ChapterBackgrounds/theBeginnings_aiImage_toBeReplaced",
                        },
                        
                    }
                },
            }
        },
    };

}
