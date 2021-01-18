using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public void PlayTimeless()
    {
        SceneManager.LoadScene("Game_Timeless");
    }
    public void PlaySurvival()
    {
        SceneManager.LoadScene("Game_Survival");
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
}