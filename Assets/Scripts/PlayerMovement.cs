using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int speed = 10;
    public Rigidbody2D rigidBody;

    // Update is called once per frame
    void Update()
    {
        // float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

        // transform.Translate(Vector2.up * (moveVertical * speed * Time.deltaTime));
        // transform.Translate(Vector2.right * (moveHorizontal * speed * Time.deltaTime));
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        // transform.position = transform.position + movement * speed * Time.deltaTime;
        rigidBody.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }
}