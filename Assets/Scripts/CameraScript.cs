using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 offsetRotated;

    // Update is called once per frame
    void Update()
    {
        Vector3 offsetVector = new Vector3(0.0f, 1.0f, -30.5f);
        
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offsetVector, 0.15f);
    }
}
