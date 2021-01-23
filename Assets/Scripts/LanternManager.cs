using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public PlugScript[] Plugs;
    public int totalReplugged;
    public bool isCompleted;
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Get the health of the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        isCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        totalReplugged = 0;
        for (int i = 0; i < Plugs.Length; i++)
        {
            if (Plugs[i].isReplugged)
                totalReplugged += 1;

        }
        if (totalReplugged == Plugs.Length && !isCompleted)
        {
            isCompleted = true;
            playerHealth.IncreaseHealth(10);
        }
    }
}
