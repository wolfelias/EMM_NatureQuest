using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanternManager : MonoBehaviour
{
    public PlugScript[] Plugs;
    public int totalReplugged;
    public bool isCompleted;
    private Health playerHealth;
    public GameObject floatingPoints;
    private Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Get the health of the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        // Get the canvas for the UI
        canvas = GameObject.Find("HealthBarCanvas").GetComponent<Transform>();
        isCompleted = false;
    }

    public void ShowCurrentLantern()
    {
        GameObject count = Instantiate(floatingPoints, canvas.position, Quaternion.identity);
        count.transform.SetParent(canvas);
        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = totalReplugged + " / " + Plugs.Length;
        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        count.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 20;
    }

    public void Replugged()
    {
        totalReplugged += 1;

        if (totalReplugged == Plugs.Length)
        {
            isCompleted = true;
            playerHealth.IncreaseHealth(10);
        }
    }

    public void WrongMove()
    {
        totalReplugged -= 1;
    }
}
