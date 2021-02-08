using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file PlugScript.cs
 *
 *  @brief A script used for plug prefab
 *
 *  @author Sunan Regi Maunakea
 *
 *  A plug prefab has a rigidbody 2D component. Player can
 *  pick up and drop a plug inside a lantern area. A plug
 *  is one of the important component in the electro minigame.
 *  It is connected to a lantern using a cable (line).
 */
public class PlugScript : MonoBehaviour
{
    public bool equipped;
    public static bool slotFull;
    public Rigidbody2D rigidBody;

    private bool isNearOutlet;
    private bool isNearSolarOutlet;
    private bool isPickable;
    public bool isPlugged;
    public bool isReplugged;

    private Transform container;
    private Transform parentTransform;
    private Vector3 outletPosition;
    private Vector3 tempPosition;
    private LanternManager lanternManager;

    /*! @brief Start method of the script
     *  
     *  Get the necessary component for the plug. Set isPlugged to true and
     *  isReplugged to false.
     */
    void Start()
    {
        lanternManager = GameObject.Find("LanternManager").GetComponent<LanternManager>();
        container = GameObject.Find("Container").transform;
        parentTransform = transform.parent;
        isPlugged = true;
        isReplugged = false;
    }

    /*! @brief Update method of the script
     *  
     *  Check every frame if player is trying to pick up or drop a plug
     *  before the game is completed
     */
    void Update()
    {
        if (!lanternManager.isCompleted)
        {
            if (!equipped && Input.GetKeyDown(KeyCode.E) && !slotFull && isPickable && !isReplugged)
                PickUp();
            if (equipped && Input.GetKeyDown(KeyCode.Q))
                Drop();
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPickable = true;
        }
        if (other.tag == "Outlet")
        {
            isNearOutlet = true;
            outletPosition = other.transform.position;
        }
        if (other.tag == "SolarOutlet")
        {
            isNearSolarOutlet = true;
            outletPosition = other.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPickable = false;
        }
        if (other.tag == "Outlet")
        {
            isNearOutlet = false;
        }
        if (other.tag == "SolarOutlet")
        {
            isNearSolarOutlet = false;
        }
        if (other.tag == "LanternArea" && equipped)
        {
            PutBack();
        }
    }

    /*! 
     *  Pick up the nearest plug by setting the plug parent to the container.
     *  Set equipped and slotFull to true, so player can't pick up another plug.
     *  Make plug a child of the container and move it to default position with
     *  a slight change on the Y-axis.
     */
    private void PickUp()
    {
        tempPosition = transform.localPosition;
        equipped = true;
        slotFull = true;
        transform.SetParent(container);
        transform.localPosition = new Vector3(0, -0.5f, 0);
        rigidBody.isKinematic = true;
        isPlugged = false;
    }

    /*!
     *  Drop the equipped plug when "Q" is pressed by setting the plug parent to
     *  its original parent. If the drop range is near an outlet, then set the
     *  position of the dropped to the position of the outlet.
     */
    private void Drop()
    {
        Detach();
        if (isNearOutlet)
        {
            transform.position = outletPosition;
            isPlugged = true;
        }
        else if (isNearSolarOutlet)
        {
            transform.position = outletPosition;
            isPlugged = true;
            isReplugged = true;
            lanternManager.Replugged();
            lanternManager.ShowCurrentLantern();
        }
        else
        {
            isPlugged = false;
        }
    }

    /*!
     *  Unequip plug if player leaves the lantern area.
     *  Set the plug position to the position during pick up.
     */
    private void PutBack()
    {
        Detach();
        transform.localPosition = tempPosition;
    }

    /*!
     *  Detach the plug from the container and set the parent to its
     *  original parent.
     */
    private void Detach()
    {
        equipped = false;
        slotFull = false;
        rigidBody.isKinematic = false;
        transform.SetParent(parentTransform);
    }
}
