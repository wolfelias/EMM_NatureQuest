using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Plane = System.Numerics.Plane;

public class PauseMenuManagerScript : MonoBehaviour
{
    public GameObject menu;
    public Button restartButton;
    public Button returnButton;
    public Button aboutButton;
    public Button newGameButton;
    public Button survivalModeButton;
    public Button timelessModeButton;
    public Button backButton;
    public Button goToMainMenButton;
    public GameObject pauseMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject aboutText;
    public GameObject TimeText;
    public GameObject LostText;

    private bool TimelessMode = true;
    private bool _menuIsActive = true;

    private TimerController timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = TimerController.Instance;

        newGameButton.onClick.AddListener(ShowGameTypeMenu);
        backButton.onClick.AddListener(ShowMainMenu);
        timelessModeButton.onClick.AddListener(StartGameTimelessMode);
        survivalModeButton.onClick.AddListener(StartGameSurvivalMode);
        restartButton.onClick.AddListener(RestartGame);
        returnButton.onClick.AddListener(ContinueGame);
        aboutButton.onClick.AddListener(ShowAboutPage);
        goToMainMenButton.onClick.AddListener(RestartGame);

        //TODO: start particular game mode
        if (_menuIsActive)
        {
            Time.timeScale = 0;
            ShowMainMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menuIsActive)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menuIsActive)
        {
            ContinueGame();
        }

        if (timer.maxTime <= TimeSpan.Zero)
        {
            Time.timeScale = 0.0f;
            ShowLostMenu();
        }
    }

    void RestartGame()
    {
        _menuIsActive = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // ContinueGame();
    }

    void StartGameImmediatly()
    {
    }

    void ShowGameTypeMenu()
    {
        aboutButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        survivalModeButton.gameObject.SetActive(true);
        timelessModeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        pauseMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(true);
        aboutText.SetActive(false);
        menu.SetActive(true);
        LostText.SetActive(false);
        _menuIsActive = true;

        Time.timeScale = 0;
    }

    void ShowMainMenu()
    {
        aboutButton.gameObject.SetActive(true);
        newGameButton.gameObject.SetActive(true);
        survivalModeButton.gameObject.SetActive(false);
        timelessModeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(false);
        LostText.SetActive(false);
        pauseMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(true);
        aboutText.SetActive(false);
        menu.SetActive(true);
        _menuIsActive = true;

        Time.timeScale = 0;
    }

    void PauseGame()
    {
        aboutButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        survivalModeButton.gameObject.SetActive(false);
        timelessModeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(true);
        LostText.SetActive(false);
        pauseMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(true);
        aboutText.SetActive(false);
        menu.SetActive(true);
        _menuIsActive = true;

        Time.timeScale = 0;
    }

    void ContinueGame()
    {
        aboutButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        survivalModeButton.gameObject.SetActive(false);
        timelessModeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(false);
        LostText.SetActive(false);
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        aboutText.SetActive(false);
        menu.SetActive(false);
        _menuIsActive = false;

        Time.timeScale = 1.0f;
    }

    void ShowAboutPage()
    {
        aboutButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        survivalModeButton.gameObject.SetActive(false);
        timelessModeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(false);
        LostText.SetActive(false);
        backButton.gameObject.SetActive(true);
        pauseMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(true);
        aboutText.SetActive(true);

        menu.SetActive(true);
        _menuIsActive = true;

        Time.timeScale = 0;
    }

    void StartGameTimelessMode()
    {
        aboutButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        survivalModeButton.gameObject.SetActive(false);
        timelessModeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(false);
        LostText.SetActive(false);
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        aboutText.SetActive(false);
        menu.SetActive(false);
        _menuIsActive = false;

        TimeText.SetActive(false);

        Time.timeScale = 1.0f;
    }

    void StartGameSurvivalMode()
    {
        aboutButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        survivalModeButton.gameObject.SetActive(false);
        timelessModeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(false);
        LostText.SetActive(false);
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        aboutText.SetActive(false);
        TimeText.SetActive(true);
        menu.SetActive(false);
        _menuIsActive = false;

        Time.timeScale = 1.0f;

        timer.BeginTimer();
    }

    void ShowLostMenu()
    {
        aboutButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        survivalModeButton.gameObject.SetActive(false);
        timelessModeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        goToMainMenButton.gameObject.SetActive(true);
        LostText.SetActive(true);
        pauseMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(true);
        aboutText.SetActive(false);
        menu.SetActive(true);
        _menuIsActive = true;

        Time.timeScale = 0;
    }
}