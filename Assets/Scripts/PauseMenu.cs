using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenu;
    public GameObject pauseFirstButton;
    private static TimerController timerController;
    private static HealthbarNotifier healthbarNotifier;

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

    private void Update()
    {
        bool temp = true;
        if (SceneManager.GetActiveScene().name.Equals("Game_Survival"))
        {
            temp = timerController.isOver;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !temp && !healthbarNotifier.isOver)
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

    public void Pause()
    {
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void LoadStartMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}