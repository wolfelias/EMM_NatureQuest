using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*! @file HealthBar.cs
 *
 *  @brief A script used for the health bar
 *
 *  @author Sunan Regi Maunakea
 *
 *  A health bar shows the current health the player has. It is located
 *  at the bottom center of the screen. The current health points is
 *  showed next to the health bar.
 */
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;
    public TextMeshProUGUI mText;

    /*! @brief Start method of the script
     *  
     *  Get the player health component
     */
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth / 2;
        mText = GameObject.Find("Health Bar Points").GetComponent<TextMeshProUGUI>();
        mText.SetText(healthBar.value +" / "+ playerHealth.maxHealth);
    }

    /*!
     *  Set the player health to the given parameter
     */
    public void SetHealth(int hp)
    {
        healthBar.value = hp;
        if (hp > healthBar.maxValue)
            hp = (int)healthBar.maxValue;
        mText.SetText(hp +" / "+ playerHealth.maxHealth);
            
    }
}
