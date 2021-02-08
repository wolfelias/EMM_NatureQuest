using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file WasteScript.cs
 *
 *  @brief A script used for waste prefab
 *
 *  @author Sunan Regi Maunakea
 *
 *  A waste prefab consists of a rigidbody 2D component and a box
 *  collider component. Player can pick up and drop a waste prefab
 *  only when player is inside the trigger area. There are 5 types
 *  of waste that the player must sort into the correct waste bin.
 */
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

    /*! @brief Start method of the script
     *  
     *  Get the player, player's health, and each bin transform
     */
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wasteContainer = GameObject.Find("Container").transform;

        recycleBin = GameObject.Find("RecycleBin").transform;
        paperBin = GameObject.Find("PaperBin").transform;
        organicBin = GameObject.Find("OrganicBin").transform;
        householdBin = GameObject.Find("HouseholdBin").transform;
        glassBin = GameObject.Find("GlassBin").transform;

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        spawnWaste = GameObject.Find("Waste Spawner").GetComponent<SpawnWaste>();
    }
    
    /*! @brief Update method of the script
     *  
     *  Check if player is in range, "E" is pressed, and if player is in the trigger area.
     *  Drop if equipped and "Q" is pressed.
     */
    private void Update()
    {
        Vector2 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull && Physics2D.OverlapCircle(player.transform.position, 0.2f, triggerLayer) != null)
            PickUp();

        if (equipped && Input.GetKeyDown(KeyCode.Q))
            Drop();
    }

    /*! 
     *  Pick up the nearest waste by setting the waste parent to the container.
     *  Set equipped and slotFull to true, so player can't pick up another waste.
     *  Make waste a child of the container and move it to default position.
     */
    private void PickUp()
    {
        equipped = true;
        slotFull = true;
        tempPosition = transform.position;

        transform.SetParent(wasteContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        rigidBody.isKinematic = true;
        boxCollider.isTrigger = true;
    }

    /*!
     *  Drop waste when "Q" is pressed. If waste is dropped near the position of
     *  the garbage can, destroy the object, if not drop on the ground. The correct
     *  disposal of the waste leads to the increase of health by 2 points. Otherwise,
     *  health decreases by 2 points.
     */
    private void Drop()
    {
        Vector2 distanceToRecycleBin = recycleBin.position - transform.position;
        Vector2 distanceToPaperBin = paperBin.position - transform.position;
        Vector2 distanceToOrganicBin = organicBin.position - transform.position;
        Vector2 distanceToHouseholdBin = householdBin.position - transform.position;
        Vector2 distanceToGlassBin = glassBin.position - transform.position;

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

    /*!
     *  Unequip waste by setting the equipped and slotFull to false.
     *  Set the parent of the waste to null.
     */
    public void Detach()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rigidBody.isKinematic = false;
        boxCollider.isTrigger = false;
    }

    /*!
     *  Decrease the amount of spawned waste when waste is destroyed.
     *  Automatically unequip waste if destroyed.
     */
    void OnDestroy()
    {
        equipped = false;
        slotFull = false;
        spawnWaste.MinusCount();
    }

    /*!
     *  Unequip waste if player leaves the trigger area. Set the waste
     *  position to its original position during pick up.
     */
    public void PutBack()
    {
        Detach();
        transform.localPosition = tempPosition;
    }
}
