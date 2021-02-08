using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file WaterpipeScript.cs
 *
 *  @brief A script used for controlling the water pipes
 *
 *  @author Sunan Regi Maunakea
 *
 *  Each water pipe has 4 degrees of rotation which are 0, 90, 180, and 270.
 *  In each puzzle, each water pipe has its own correct rotation. At first,
 *  the water pipes will be rotated to a random rotation. Player can only
 *  rotate the pipes while inside the collider of the water pipe which the
 *  player intends to rotate.
 */

public class WaterpipeScript : MonoBehaviour
{
    private float[] rotations = { 0, 90, 180, 270 };
    public float[] correctRotation;
    [SerializeField]
    private bool isPlaced = false;
    private int possibleRots = 1;
    private WaterPipeManager wpManager;

    private bool rotateAllowed;

    /*! 
     *  Retrieve the water pipe manager component before Start() method
     */
    private void Awake()
    {
        wpManager = GameObject.Find("WaterPipeManager").GetComponent<WaterPipeManager>();
    }

    /*! @brief Start method of the script
     *  
     *  Check the possible rotation of a pipe by getting the length of the correct rotation.
     *  Set the random rotation for each pipe.
     */
    void Start()
    {
        possibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (possibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaced = true;
                wpManager.CorrectMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isPlaced = true;
                wpManager.CorrectMove();
            }
        }
    }

    /*! @brief Update method of the script
     *  
     *  Player can rotate the pipes when behind the pipe and key 'E' is pressed.
     *  If the pipe set is not yet completed then player can rotate.
     */
    void Update()
    {
        if (rotateAllowed && Input.GetKeyDown(KeyCode.E))
        {
            if (!wpManager.isCompleted)
            {
                RotatePipe();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rotateAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rotateAllowed = false;
        }
    }

    /*!
     *  Rotate the pipe counter-clockwise for each 90 degrees
     */
    private void RotatePipe()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.eulerAngles.z));

        if (possibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1] && !isPlaced)
            {
                isPlaced = true;
                wpManager.CorrectMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                wpManager.WrongMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0] && !isPlaced)
            {
                isPlaced = true;
                wpManager.CorrectMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                wpManager.WrongMove();
            }
        }
    }
}
