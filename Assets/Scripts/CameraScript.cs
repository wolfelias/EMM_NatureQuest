using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Quaternion rotation;
    private Vector3 offsetRotated;
    public float speed;
    public float distance;



    // Update is called once per frame
    void Update()
    {
        Vector3 offsetVector = new Vector3(0.0f, 0.0f, -distance);
        
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offsetVector,
            speed);
    }
}
