using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public MinigamesManager minigamesManager;
    public Button chillButton;
    public Button survivalButton;
    public GameObject survivalText;
    public GameObject chillText;

    private void Start()
    {
        survivalText.gameObject.SetActive(false);
        chillText.gameObject.SetActive(false);
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