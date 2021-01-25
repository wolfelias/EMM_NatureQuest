using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarNotifier : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    public GameObject lostPanel;
    public GameObject wonPanel;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    
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
        if (playerHealth.curHealth == 0)
        {
            lostPanel.SetActive(true);
            restartButton.SetActive(true);
            mainMenuButton.SetActive(true);
        }
        else if (playerHealth.curHealth == 100)
        {
            wonPanel.SetActive(true);
            restartButton.SetActive(true);
            mainMenuButton.SetActive(true);
        }
    }
}