using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Quaternion rotation;
    private Vector3 offsetRotated;

    public int interpolationFramesCount = 10; // Number of frames to completely interpolate between the 2 positions
    int thirdPersonElapsedFrames = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offsetVector = new Vector3(0.0f, 1.0f, -20.5f);
        transform.rotation = player.transform.rotation;
        offsetRotated = player.transform.rotation * offsetVector;
        float interpolationRatio = (float) thirdPersonElapsedFrames / interpolationFramesCount;
        transform.position = Vector3.Lerp(player.transform.position, player.transform.position + offsetRotated, interpolationRatio);
        thirdPersonElapsedFrames++;
    }
}