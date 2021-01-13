﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int minHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecreaseHealth(int damage)
    {
        if (curHealth > minHealth)
        {
            curHealth -= damage;
            healthBar.SetHealth(curHealth);
        }
    }

    public void IncreaseHealth(int hp)
    {
        if (curHealth < maxHealth)
        {
            curHealth += hp;
            healthBar.SetHealth(curHealth);
        }
    }

    public int CurHealth { get { return curHealth; } }
}