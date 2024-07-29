using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class infoBox : MonoBehaviour
{
    //public variables
    public string miniGame = "grid";

    //ui
    [SerializeField] private TextMeshProUGUI diffText, startText;
    [SerializeField] private GameObject modifListUI;
    [SerializeField] private Button initButton;

    [SerializeField] public GameObject gamePrefab;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();

        diffText.gameObject.SetActive(false);
        modifListUI.gameObject.SetActive(false);
        startText.gameObject.SetActive(true);
        initButton.gameObject.SetActive(false);
    }

    //difficulty setter function for buttons
    private int difficulty = 0;

    public void setDifficulty(int diffToSet)
    {
        difficulty = diffToSet;

        var textDiff = "Easy";
        switch (difficulty)
        {
            case 0: textDiff = "Easy"; break;
            case 1: textDiff = "Medium"; break;
            case 2: textDiff = "Hard"; break;
            case 3: textDiff = "ULTRA HARD"; break;
        }
        diffText.text = textDiff;

        //update infoBox's values
        foreach (Transform multiText in modifListUI.transform)
        {
            multiText.gameObject.GetComponent<gameStatDisplay>().value = gameManager.gameData.endlessSettings[miniGame][difficulty].GetProperty(multiText.name);
        }

        startText.gameObject.SetActive(false);
        diffText.gameObject.SetActive(true);
        modifListUI.gameObject.SetActive(true);
        initButton.gameObject.SetActive(true);
    }

    //initiate game
    public void initGame()
    {
        var newGridGame = Instantiate(gamePrefab, gameManager.canvas.transform);
        var gridLogic = newGridGame.GetComponent<miniGame>();

        //set the game modifiers; to be modified based on game and in relation with gameSettings class
        //TODO: incorporate MiniGameSettings class in miniGame!!!!!!!!
        gridLogic.difficulty = gameManager.gameData.endlessSettings[miniGame][difficulty].Difficulty;
        gridLogic.maxTime = gameManager.gameData.endlessSettings[miniGame][difficulty].MaxTime;
        gridLogic.sizeMulti *= gameManager.gameData.endlessSettings[miniGame][difficulty].SizeMulti;
        gridLogic.scoreMulti *= gameManager.gameData.endlessSettings[miniGame][difficulty].ScoreMulti;
        gridLogic.imageNrMulti *= gameManager.gameData.endlessSettings[miniGame][difficulty].ImageNrMulti;
        gridLogic.timeMulti *= gameManager.gameData.endlessSettings[miniGame][difficulty].TimeMulti;
        gridLogic.resetTimerMulti *= (int)gameManager.gameData.endlessSettings[miniGame][difficulty].ResetTimerMulti;

        //start game
        gridLogic.startGame();
    }
}