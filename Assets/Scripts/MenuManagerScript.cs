using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Plane = System.Numerics.Plane;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject menu;
    public Button restartButton;
    public Button returnButton;
    public GameObject menuPanel;
    private bool _menuIsActive = false;


    // Start is called before the first frame update
    void Start()
    {
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        menuPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        returnButton.onClick.AddListener(ContinueGame);
        returnButton.GetComponentInChildren<Text>().text = "RETURN";
        restartButton.GetComponentInChildren<Text>().text = "RESTART";
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
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ContinueGame();
    }

    void PauseGame()
    {
        menu.SetActive(true);
        _menuIsActive = true;
        menuPanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void ContinueGame()
    {
        menu.SetActive(false);
        _menuIsActive = false;
        menuPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}