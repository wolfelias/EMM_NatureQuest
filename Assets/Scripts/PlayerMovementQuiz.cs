using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    interact
}

public class PlayerMovementQuiz : MonoBehaviour
{
    private PlayerState currentState;

    public float speed;
    private Rigidbody2D rb;
    private Vector3 change;
    private Animator animator;

    public PlayerState CurrentState { 
        get { return currentState; }
        set { currentState = value; }
    }

    private void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        if (currentState == PlayerState.walk)
        {
            UpdateAnimation();
        }       
    }

    void UpdateAnimation()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void MoveCharacter()
    {
        // Normalize 'change' to make diagonal movement slower
        change.Normalize();
        rb.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}