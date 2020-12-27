using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteScript : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public BoxCollider2D collider;
    public Transform player, wasteContainer;

    public float pickUpRange;
    public bool equipped;
    public static bool slotFull;

    public float dropRange;
    public Transform plasticBin, paperBin, organicBin,
    hazardousBin, glassBin;

    private Health playerHealth;

    private void Start()
    {
        // Get the health of the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        // Setup
        if (!equipped)
        {
            rigidbody.isKinematic = false;
            collider.isTrigger = false;
        }
        else
        {
            rigidbody.isKinematic = true;
            collider.isTrigger = true;
            slotFull = true;
        }
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
        transform.localScale = Vector3.one;

        // Make Rigidbody kinematic and BoxCollider a trigger
        rigidbody.isKinematic = true;
        collider.isTrigger = true;

        // Unequipped object if player leaves the trigger area
        // if (gameObject == null)
        // {
        //     Debug.Log("Waste destroyed");
        //     equipped = false;
        //     slotFull = false;
        // }
    }

    private void Drop()
    {
        Vector2 distanceToPlasticBin = plasticBin.position - transform.position;
        Vector2 distanceToPaperBin = paperBin.position - transform.position;
        Vector2 distanceToOrganicBin = organicBin.position - transform.position;
        Vector2 distanceToHazardousBin = hazardousBin.position - transform.position;
        Vector2 distanceToGlassBin = glassBin.position - transform.position;

        // If waste dropped near the position of the garbage can,
        // destroy the object, if not drop on the ground
        if (distanceToPlasticBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (tag == "PlasticWaste")
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
            if (tag == "PaperWaste")
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
            if (tag == "OrganicWaste")
            {
                playerHealth.IncreaseHealth(5);
            }
            else
            {
                playerHealth.DecreaseHealth(5);
            }
        }
        else if (distanceToHazardousBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (tag == "HazardousWaste")
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
            if (tag == "GlassWaste")
            {
                playerHealth.IncreaseHealth(5);
            }
            else
            {
                playerHealth.DecreaseHealth(5);
            }
        }

        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        // Make Rigidbody not kinematic and BoxCollider normal
        rigidbody.isKinematic = false;
        collider.isTrigger = false;
    }
}
