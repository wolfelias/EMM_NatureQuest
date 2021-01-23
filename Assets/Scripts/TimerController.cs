﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public int time;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTime = TimeSpan.FromMinutes(time);
        timeCounter.text = "Time 05:00.00";
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
            TimeSpan timeSpan = TimeSpan.FromSeconds(30);
            maxTime -= TimeSpan.FromSeconds(Time.deltaTime);
            if (TimeSpan.Compare(maxTime, timeSpan) == -1)
            {
                timeCounter.text = "Time 05:00.00";
                timeCounter.color = new Color(255, 0, 0, 100);
            }

            string timePlayingStr = "Time: " + maxTime.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}