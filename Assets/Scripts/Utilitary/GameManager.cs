using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;
    public GameData gameData;

    void Start()
    {
        ShowPanel("start");
        LoadGame();
    }

    void LoadGame()
    {
        //Load player data
        playerData = SaveSystem.LoadPlayerData();

        //Load game data
        gameData = SaveSystem.LoadGameData();

        //initiate tweening module
        DOTween.Init().SetCapacity(8192, 512);
    }

    void SaveGame()
    {
        //Save player data
        SaveSystem.SavePlayerData(playerData);
    }

    public void Quit()
    {
        // If we are running in the Unity editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
        OnApplicationQuit();
#else
        // If we are running in a build, quit the application
        Application.Quit();
#endif
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    #region Navigation Functions
    public Canvas canvas;
    [SerializeField] private string activePanel = "start";
    [SerializeField] private List<GameObject> panels;

    public void ShowPanel(string panelName)
    {
        activePanel = panelName;
        foreach (GameObject panel in panels)
        {
            if (panel.name == panelName)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }
    public void ShowPanel()
    {
        foreach (GameObject panel in panels)
        {
            if (panel.name == activePanel)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

    public void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    #endregion

    #region Settings
    public void SetVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SoundVolume", slider.value);
        PlayerPrefs.Save();
    }
    public void SetParticles(Slider slider)
    {
        PlayerPrefs.SetInt("Particles", (int)slider.value);
        PlayerPrefs.Save();
    }
    public void SetDiaLang(string lang)
    {
        PlayerPrefs.SetString("DiaLang", lang);
        PlayerPrefs.Save();
    }
    #endregion

    #region Tutorial
    [SerializeField] private GameObject dialoguePrefab;
    private bool once = true;
    public void CheckTutorialOnStart()
    {
        if (!playerData.playedTutorial && once)
        {
            ShowPanel("menu_tutorial");
            activePanel = "menu_main";
            once = false;
        } else
        {
            ShowPanel("menu_main");
        }
    }
    public void DisplayTutorial()
    {
        HideAllPanels();
        if (!playerData.playedTutorial)
        {
            var dialogue = Instantiate(dialoguePrefab, canvas.transform);
            dialogue.GetComponent<dialogueMaaster>().DisplayLevelStory("Tutorial", 0);
            dialogue.GetComponent<dialogueMaaster>().SequenceFinished.AddListener(() =>
            {
                playerData.playedTutorial = true;
                dialogue = Instantiate(dialoguePrefab, canvas.transform);
                dialogue.GetComponent<dialogueMaaster>().DisplayLevelStory("Tutorial", 1);
                dialogue.GetComponent<dialogueMaaster>().SequenceFinished.AddListener(ShowPanel);
            });
        } else
        {
            var dialogue = Instantiate(dialoguePrefab, canvas.transform);
            dialogue.GetComponent<dialogueMaaster>().DisplayLevelStory("Tutorial", 1);
            dialogue.GetComponent<dialogueMaaster>().SequenceFinished.AddListener(ShowPanel);
        }
    }
    #endregion
}
