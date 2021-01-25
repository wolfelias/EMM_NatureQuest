﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;

    public Text timeCounter;

    private TimeSpan timePlaying;

    private bool timerGoing;

    private float elapsedTime;

    private bool gamePlaying;

    public TimeSpan maxTime;

    public double time;

    public GameObject lostPanel;

    public GameObject restartButton;

    public GameObject mainMenuButton;

    private bool isOver = false;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTime = TimeSpan.FromMinutes(time);
        timeCounter.text = "Time";
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    void EndTimer()
    {
        timerGoing = false;
    }


    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            maxTime -= TimeSpan.FromSeconds(Time.deltaTime);
            string timePlayingStr = "Time: " + maxTime.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            if (TimeSpan.Compare(maxTime, TimeSpan.FromSeconds(20)) == -1)
            {
                timeCounter.color = new Color(255, 0, 0, 100);
            }

            if ((TimeSpan.Compare(maxTime, TimeSpan.FromSeconds(0)) == 0 ||
                 TimeSpan.Compare(maxTime, TimeSpan.FromSeconds(0)) == -1) && !isOver)
            {
                Time.timeScale = 0;
                timeCounter.text = "YOU LOST";
                lostPanel.SetActive(true);

                mainMenuButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(restartButton);
                
                isOver = true;
            }

            yield return null;
        }
    }
}