using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int minHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;
    public GameObject floatingPoints;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth / 2;
    }

    public void DecreaseHealth(int damage)
    {
        if (curHealth > minHealth)
        {
            GameObject points = Instantiate(floatingPoints, healthBar.transform.position, Quaternion.identity) as GameObject;
            points.transform.SetParent(healthBar.transform);
            points.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "- " + damage;
            points.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;

            curHealth -= damage;
            healthBar.SetHealth(curHealth);
        }
    }

    public void IncreaseHealth(int hp)
    {
        if (curHealth < maxHealth)
        {
            GameObject points = Instantiate(floatingPoints, healthBar.transform.position, Quaternion.identity) as GameObject;
            points.transform.SetParent(healthBar.transform);
            points.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ " + hp;
            points.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
            
            curHealth += hp;
            healthBar.SetHealth(curHealth);
        }
    }

    public int CurHealth { get { return curHealth; } }

    internal void SetHealth(int v)
    {
        curHealth = v;
    }
}
