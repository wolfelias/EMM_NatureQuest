using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthbarNotifier : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    public GameObject lostPanel;
    public GameObject wonPanel;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    private bool isGameEnded;

    // Update is called once per frame
    private void Start()
    {
        lostPanel.SetActive(false);
        wonPanel.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    void Update()
    {
        if (!isGameEnded)
        {
            if (playerHealth.curHealth == 0)
            {
                Time.timeScale = 0;
                lostPanel.SetActive(true);
                restartButton.SetActive(true);
                mainMenuButton.SetActive(true);
                isGameEnded = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(restartButton);
            }
            else if (playerHealth.curHealth == 100)
            {
                Time.timeScale = 0;
                wonPanel.SetActive(true);
                restartButton.SetActive(true);
                mainMenuButton.SetActive(true);
                isGameEnded = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(restartButton);
            }
        }
    }
}