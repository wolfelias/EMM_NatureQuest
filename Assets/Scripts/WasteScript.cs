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
    private Vector3 tempPosition;

    public float dropRange;
    private Transform recycleBin, paperBin, organicBin,
    householdBin, glassBin;

    private Health playerHealth;
    public SpawnWaste spawnWaste;
    public LayerMask triggerLayer;

    private void Start()
    {
        // Get the player transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wasteContainer = GameObject.Find("Container").transform;

        // Get each bin transform
        recycleBin = GameObject.Find("RecycleBin").transform;
        paperBin = GameObject.Find("PaperBin").transform;
        organicBin = GameObject.Find("OrganicBin").transform;
        householdBin = GameObject.Find("HouseholdBin").transform;
        glassBin = GameObject.Find("GlassBin").transform;

        // Get the health of the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        spawnWaste = GameObject.Find("Waste Spawner").GetComponent<SpawnWaste>();
    }

    private void Update()
    {
        // Check if player is in range, "E" is pressed, and if player is in the trigger area
        Vector2 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull && Physics2D.OverlapCircle(player.transform.position, 0.2f, triggerLayer) != null)
            PickUp();

        // Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
            Drop();
    }

    // Pick up the nearest waste by setting the waste parent to the container
    // Set equipped and slotFull to true, so player can't pick up another waste
    private void PickUp()
    {
        equipped = true;
        slotFull = true;
        tempPosition = transform.position;

        // Make waste a child of the camera and move it to default position
        transform.SetParent(wasteContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        // Make Rigidbody kinematic and BoxCollider a trigger
        rigidBody.isKinematic = true;
        boxCollider.isTrigger = true;
    }

    // Drop waste when "Q" is pressed
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
            if (CompareTag("RecyclableMaterial")) playerHealth.IncreaseHealth(2);
            else playerHealth.DecreaseHealth(2);
        }
        else if (distanceToPaperBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("Paper")) playerHealth.IncreaseHealth(2);
            else playerHealth.DecreaseHealth(2);
        }
        else if (distanceToOrganicBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("OrganicWaste")) playerHealth.IncreaseHealth(2);
            else playerHealth.DecreaseHealth(2);
        }
        else if (distanceToHouseholdBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("HouseholdWaste")) playerHealth.IncreaseHealth(2);
            else playerHealth.DecreaseHealth(2);
        }
        else if (distanceToGlassBin.magnitude <= dropRange)
        {
            Destroy(gameObject);
            if (CompareTag("GlassWaste")) playerHealth.IncreaseHealth(2);
            else playerHealth.DecreaseHealth(2);
        }

        Detach();
    }

    public void Detach()
    {
        // Unequipped waste is dropped
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        // Make Rigidbody not kinematic and BoxCollider normal
        rigidBody.isKinematic = false;
        boxCollider.isTrigger = false;
    }

    void OnDestroy()
    {
        // Unequipped waste if destroyed
        equipped = false;
        slotFull = false;
        spawnWaste.MinusCount();
    }

    // Unequipped waste if player leaves the trigger area
    // Set the waste position to the position during pick up
    public void PutBack()
    {
        Detach();
        transform.localPosition = tempPosition;
    }
}
