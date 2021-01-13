using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeWithering : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    void Update()
    {
        animator.SetInteger("Health", playerHealth.CurHealth);
    }
}
