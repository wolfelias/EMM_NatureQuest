using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterpipeScript : MonoBehaviour
{
    private float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;
    [SerializeField]
    private bool isPlaced = false;
    private int possibleRots = 1;
    private WaterPipeManager wpManager;

    private bool rotateAllowed;

    private void Awake()
    {
        // Get the water pipe manager component
        wpManager = GameObject.Find("WaterPipeManager").GetComponent<WaterPipeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Check the possible rotation of a pipe by getting the length of the correct rotation
        possibleRots = correctRotation.Length;

        // A random int to set the random rotation
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        // If possible rotation bigger than 1 (for lines)
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

    void Update()
    {
        // Player can rotate the pipes when behind the pipe and key 'E' is pressed
        if (rotateAllowed && Input.GetKeyDown(KeyCode.E))
        {
            // If the pipe set is not yet completed then player can rotate
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

    // Rotating the pipe counter-clockwise for each 90 degrees
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
