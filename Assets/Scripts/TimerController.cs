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
        timerGoing = false;
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

            yield return null;
        }
    }
}