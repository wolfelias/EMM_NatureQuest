using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
*   Two PlayerStates:
*   walk: while walking
*   interact: while interacting with sth
*/
public enum PlayerState
{
    walk,
    interact
}

    /**
    *   Movement Script for the player
    */
public class PlayerMovement : MonoBehaviour
{
    private PlayerState currentState;

    public float speed;
    private Rigidbody2D rb;
    private Vector3 change;
    private Animator animator;
    private Vector2 startPosition = new Vector2(-61, -17);

    public PlayerState CurrentState { 
        get { return currentState; }
        set { currentState = value; }
    }

    /**
    *   set Startposition of the player and get his animator and rigidbody, sets currentState to walk
    */
    private void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
    }

    /**
    *   x and y values of the change - Vector3 are set to the player input values and calls the UpdateAnimation when playerState = walk
    */
    private void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        if (currentState == PlayerState.walk)
        {
            UpdateAnimation();
        }       
    }

    /**
    *   calls MoveCharacter
    *   sets animator parameters for animation  
    */
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

    /**
    *   calls change.Normalize so diagonal movement won't be faster than non-diagonal movements
    *   sets position of player to new position  
    */
    void MoveCharacter()
    {
        // Normalize 'change' to make diagonal movement slower
        change.Normalize();
        
        //rb.MovePosition(transform.position + change * (speed * Time.deltaTime));
        rb.position = Vector3.Lerp(transform.position, transform.position + change* (speed * Time.deltaTime),
            0.5f);
    }
}