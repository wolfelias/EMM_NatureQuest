using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
*   manages animator of tree to always see the health of player/world and change the animations
*/
public class TreeWithering : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Health playerHealth;

    
    /**
    *   get playerHealth
    */
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    /**
    *   update animator parameter "Health" to value of players current health
    */
    void Update()
    {
        animator.SetInteger("Health", playerHealth.CurHealth);
    }
}
