using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;
    public TextMeshProUGUI mText;

    // Start is called before the first frame update
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth / 2;
        mText = GameObject.Find("Health Bar Points").GetComponent<TextMeshProUGUI>();
        mText.SetText(healthBar.value +" / "+ playerHealth.maxHealth);
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
        if (hp > healthBar.maxValue)
            hp = (int)healthBar.maxValue;
        mText.SetText(hp +" / "+ playerHealth.maxHealth);
            
    }
}
