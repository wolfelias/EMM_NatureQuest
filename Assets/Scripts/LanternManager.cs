using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*! @file LanternManager.cs
 *
 *  @brief A script used for the managing the electro minigame
 *
 *  @author Sunan Regi Maunakea
 *
 *  Electro minigame is one of the minigames in Nature Quest, where
 *  player must find all the lanterns in the map and replugged the
 *  plug from a normal outlet to a solar outlet. There are in total
 *  8 lanterns to be found. After replugging all the lanterns, player
 *  will get 10 health points.
 */
public class LanternManager : MonoBehaviour
{
    public PlugScript[] Plugs;
    public int totalReplugged;
    public bool isCompleted;
    private Health playerHealth;
    public GameObject floatingPoints;
    private Transform canvas;


    /*! @brief Start method of the script
     *  
     *  Get the player health and the canvas for the UI
     */
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        canvas = GameObject.Find("HealthBarCanvas").GetComponent<Transform>();
        isCompleted = false;
    }

    /*!
     *  Show the number of replugged lantern using the floating text effect
     */
    public void ShowCurrentLantern()
    {
        GameObject count = Instantiate(floatingPoints, canvas.position, Quaternion.identity);
        count.transform.SetParent(canvas);
        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = totalReplugged + " / " + Plugs.Length;
        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 20;
    }

    /*!
     *  Increase the number of total replugged lantern if the current lantern
     *  is replugged to the solar outlet
     */
    public void Replugged()
    {
        totalReplugged += 1;

        if (totalReplugged == Plugs.Length)
        {
            isCompleted = true;
            playerHealth.IncreaseHealth(10);
        }
    }

    /*!
     *  Decrease the number of total replugged lantern if the current lantern
     *  is plugged to the normal socket
     */
    public void WrongMove()
    {
        totalReplugged -= 1;
    }
}
