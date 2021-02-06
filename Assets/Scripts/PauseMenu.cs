using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

    /**
    *   handles Pause Menu
    */
public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenu;
    public GameObject pauseFirstButton;
    private static TimerController timerController;
    private static HealthbarNotifier healthbarNotifier;
    private bool allowPauseMenu = true;

    /**
    *   set TimeScale to 1
    *   initialize PauseMenu and set Timer if SurvivalMode is played
    *   get reference to the health bar
    */
    private void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        IsPaused = false;

        if (SceneManager.GetActiveScene().name.Equals("Game_Survival"))
        {
            GameObject Timer = GameObject.Find("TimeCanvas");
            timerController = Timer.GetComponent<TimerController>(); 
        }
       
        GameObject Healthbar = GameObject.Find("Health Bar");
        healthbarNotifier = Healthbar.GetComponent<HealthbarNotifier>();
        Debug.Log(healthbarNotifier.isOver);
    }

    /**
    *   pauses the menu on pressing ESC 
    *   in survival mode just possible as long timer > 0 
    */
    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game_Survival"))
        {
            allowPauseMenu = !timerController.isOver;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && allowPauseMenu && !healthbarNotifier.isOver)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /**
    *   pause the game; timeScale set to 0 
    */
    public void Pause()
    {
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        Time.timeScale = 0;
        IsPaused = true;
    }

    /**
    *   resume game; timeScale set to 1
    */
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    /**
    *   get back to startMenu 
    */
    public void LoadStartMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    /**
    *   restart current GameMode
    */
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}