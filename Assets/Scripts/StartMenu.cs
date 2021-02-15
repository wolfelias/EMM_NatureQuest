using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


    /**
    *   handles StartMenu
    */
public class StartMenu : MonoBehaviour
{
    public MinigamesManager minigamesManager;
    public Button chillButton;
    public Button survivalButton;
    public GameObject survivalText;
    public GameObject chillText;
    public GameObject menuFirstButton, modeFirstButton, aboutFirstButton, aboutClosedButton; 

    /**
    *   initialize start menu; deactivate all Buttons that aren't needed ad beginning
    */
    private void Start()
    {
        survivalText.gameObject.SetActive(false);
        chillText.gameObject.SetActive(false);
    }

    /**
    *   show descriptions of Survival- / Chill- Mode when player hovers over corresponding button
    */
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

    /**
    *   open the Mode-Selecting-Menu
    */
    public void OpenChooseMode()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(modeFirstButton);
    }

    /**
    *   get back to main screen
    */
    public void CloseChooseMode()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }

    /**
    *   open about text
    */
    public void OpenAbout()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(aboutFirstButton);
    }

    /**
    *   close the about text
    */
    public void CloseAbout()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(aboutClosedButton);
    }

    /**
    *   display info text from survival-mode-button  
    */
    public void ShowSurvivalInfo()
    {
        survivalText.SetActive(true);
    }

    /**
    *   closes info text from survival-mode-button  
    */
    public void ExitSurvivalInfo()
    {
        survivalText.SetActive(false);
    }

    /**
    *   display info text from chill-mode-button
    */
    public void ShowChillInfo()
    {
        chillText.SetActive(true);
    }

    /**
    *   closes info text from chill-mode-button
    */
    public void ExitChillInfo()
    {
        chillText.SetActive(false);
    }

    /**
    *   loads chill mode
    */
    public void PlayTimeless()
    {
        SceneManager.LoadScene("Game_Timeless");
        minigamesManager.isChill = true;
    }
    /**
    *   loads survival mode
    */
    public void PlaySurvival()
    {
        SceneManager.LoadScene("Game_Survival");
        minigamesManager.isChill = false;
    }
    /**
    *   close whole application
    */
    public void QuitGame()
    {
        Application.Quit(); 
    }
    
}