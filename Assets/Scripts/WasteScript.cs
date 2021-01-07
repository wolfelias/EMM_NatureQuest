using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;
    private Transform player, wasteContainer;

    public float pickUpRange;
    public bool equipped;
    public static bool slotFull;

    public float dropRange;
    private Transform recycleBin, paperBin, organicBin,
    householdBin, glassBin;

    private Health playerHealth;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        wasteContainer = GameObject.Find("WasteContainer").transform;
        recycleBin = GameObject.Find("RecycleBin").transform;
        paperBin = GameObject.Find("PaperBin").transform;
        organicBin = GameObject.Find("OrganicBin").transform;
        householdBin = GameObject.Find("HouseholdBin").transform;
        glassBin = GameObject.Find("GlassBin").transform;

        // Get the health of the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        // Check if player is in range and "E" is pressed
        Vector2 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        // Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        // Make waste a child of the camera and move it to default position
        transform.SetParent(wasteContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        // transform.localScale = Vector3.one;

        // Make Rigidbody kinematic and BoxCollider a trigger
        rigidBody.isKinematic = true;
        boxCollider.isTrigger = true;
    }

    private void Drop()
    {
        Vector2 distanceToRecycleBin = recycleBin.position - transform.position;
        Vector2 distanceToPaperBin = paperBin.position - transform.position;
        Vector2 distanceToOrganicBin = organicBin.position - transform.position;
        Vector2 distanceToHouseholdBin = householdBin.position - transform.position;
        Vector2 distanceToGlassBin = glassBin.position - transform.position;

        // If waste dropped near the position of the garbage can,
        // destroy the object, if not drop on the ground
        if (distanceToRecycleBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("RecyclableMaterial"))
            {
                playerHealth.IncreaseHealth(5);
            }
            else
            {
                playerHealth.DecreaseHealth(5);
            }
        }
        else if (distanceToPaperBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("Paper"))
            {
                playerHealth.IncreaseHealth(5);
            }
            else
            {
                playerHealth.DecreaseHealth(5);
            }
        }
        else if (distanceToOrganicBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("OrganicWaste"))
            {
                playerHealth.IncreaseHealth(5);
            }
            else
            {
                playerHealth.DecreaseHealth(5);
            }
        }
        else if (distanceToHouseholdBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("HouseholdWaste"))
            {
                playerHealth.IncreaseHealth(5);
            }
            else
            {
                playerHealth.DecreaseHealth(5);
            }
        }
        else if (distanceToGlassBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("GlassWaste"))
            {
                playerHealth.IncreaseHealth(5);
            }
            else
            {
                playerHealth.DecreaseHealth(5);
            }
        }
        Detach();
    }

    public void Detach()
    {
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        // Make Rigidbody not kinematic and BoxCollider normal
        rigidBody.isKinematic = false;
        boxCollider.isTrigger = false;
    }
}
