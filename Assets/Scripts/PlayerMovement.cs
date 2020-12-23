using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int speed = 10;

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.Translate(Vector2.up * (moveVertical * speed * Time.deltaTime));
        transform.Translate(Vector2.right * (moveHorizontal * speed * Time.deltaTime));
    }
}