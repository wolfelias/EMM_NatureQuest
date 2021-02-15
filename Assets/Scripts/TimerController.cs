using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

    /**
    *   manages timer in survival mode
    */
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

    public bool isOver = false;



    private void Awake()
    {
        Instance = this;
    }

    /**
    *   initialize timer and start it
    */
    void Start()
    {
        maxTime = TimeSpan.FromMinutes(time);
        timeCounter.text = "Time";
        BeginTimer();
    }

    /**
    *   begins the timer; timerGoing = true
    *   start coroutine to update timer  
    */
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    /**
    *   sets timerGoing = false when timer runs out  
    */
    void EndTimer()
    {
        timerGoing = false;
    }


    /**
    *   calls down the time
    *   when elapsedTime = maxTime activate LostMenu
    */
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