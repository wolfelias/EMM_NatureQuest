using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/*! @file Health.cs
 *
 *  @brief A script used for player's health
 *
 *  @author Sunan Regi Maunakea
 *
 *  A player has a maximum health of 100 and a minimum of 0.
 *  If health reaches the maximum health, then player won.
 *  If health reaches the minimum health, then player lost.
 */
public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int minHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;
    public GameObject floatingPoints;

    /*! @brief Start method of the script
     *  
     *  Set the current health to 50
     */
    void Start()
    {
        curHealth = maxHealth / 2;
    }

    /*!
     *  Decrease the player health for the given amount of damage
     */
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

    /*!
     *  Increase the player health for the given amount of HP
     */
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
