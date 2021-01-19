using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private bool firstPerson;
    public GameObject player;
    private Quaternion rotation;
    private Vector3 offsetRotated;

    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int thirdPersonElapsedFrames = 0;
    int firstPersonElapsedFrames = 0;


    // Start is called before the first frame update
    void Start()
    {
        firstPerson = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C) && firstPerson == true)
        {
            firstPerson = false;
        }

        else if (Input.GetKeyDown(KeyCode.C) && firstPerson == false)
        {
            firstPerson = true;
        }

        if (firstPerson == false)
        {
            firstPersonElapsedFrames = 0;
            Vector3 offsetVector = new Vector3(0.0f, 1.0f, -3.5f);
            transform.rotation = player.transform.rotation;
            offsetRotated = player.transform.rotation * offsetVector;
            float interpolationRatio = (float) thirdPersonElapsedFrames / interpolationFramesCount;
            transform.position = Vector3.Lerp(player.transform.position, player.transform.position + offsetRotated, interpolationRatio);
            thirdPersonElapsedFrames++;
           
        }

        else if (firstPerson == true)
        {
            thirdPersonElapsedFrames = 0;
            // transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
            
            float firstPersonInterpolationRatio = (float) firstPersonElapsedFrames / interpolationFramesCount;
            transform.position = Vector3.Lerp(player.transform.position + offsetRotated, player.transform.position, firstPersonInterpolationRatio);
            firstPersonElapsedFrames++;
        }
    }
}