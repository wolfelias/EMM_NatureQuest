using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // private int cnt = 0; 
    public int speed = 10;
    public LayerMask interactableLayer, solidObjectLayer;
    public Rigidbody2D rb;

    private Vector2 input;
    private Animator animator;
    private bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(!isMoving)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            // no diagonal movement
            if (input.x != 0) input.y = 0;

            //transform.Translate(Vector2.up * (moveVertical * speed * Time.deltaTime));
            //transform.Translate(Vector2.right * (moveHorizontal * speed * Time.deltaTime));

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                    //Move();   // Kein Hängenbleiben an collidern bzw. "wackeln", aber animation funzt nicht bzw. muss anders erreicht werden
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (CheckInteractInFront())
        {

        }

        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }

    private void Interact()
    {  
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, interactableLayer);
        if(collider != null) {
            collider.GetComponent<Interactable_Interface>()?.Interact();            
        }        
    }

    private bool CheckInteractInFront()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, interactableLayer);
        if (collider != null)
        {
            return true;
        }
        return false;
    }

    /*
    private void Move()
    {
        isMoving = true;
        rb.velocity = new Vector2(input.x, input.y);
        isMoving = false;
    }
    */

    IEnumerator Move(Vector3 targetPos) {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.3f, solidObjectLayer | interactableLayer) != null)
        {
            return false;
        }
            return true;
    }


}