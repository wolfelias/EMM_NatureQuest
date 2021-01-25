using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public MinigamesManager minigamesManager;
    public Button chillButton;
    public Button survivalButton;
    public GameObject survivalText;
    public GameObject chillText;
    public GameObject menuFirstButton, modeFirstButton, aboutFirstButton, aboutClosedButton; 

    private void Start()
    {
        survivalText.gameObject.SetActive(false);
        chillText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == survivalButton.gameObject)
        {
            ShowSurvivalInfo();
            ExitChillInfo();
        }
        else if (EventSystem.current.currentSelectedGameObject == chillButton.gameObject)
        {
            ShowChillInfo();
            ExitSurvivalInfo();
        }
        else
        {
            ExitSurvivalInfo();
            ExitChillInfo();
        }
    }

    public void OpenChooseMode()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(modeFirstButton);
    }

    public void CloseChooseMode()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }

    public void OpenAbout()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(aboutFirstButton);
    }

    public void CloseAbout()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(aboutClosedButton);
    }

    public void ShowSurvivalInfo()
    {
        survivalText.SetActive(true);
    }

    public void ExitSurvivalInfo()
    {
        survivalText.SetActive(false);
    }

    public void ShowChillInfo()
    {
        chillText.SetActive(true);
    }

    public void ExitChillInfo()
    {
        chillText.SetActive(false);
    }

    public void PlayTimeless()
    {
        SceneManager.LoadScene("Game_Timeless");
        minigamesManager.isChill = true;
    }
    public void PlaySurvival()
    {
        SceneManager.LoadScene("Game_Survival");
        minigamesManager.isChill = false;
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
    
}