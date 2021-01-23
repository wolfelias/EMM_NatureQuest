using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        lanternManager = GameObject.Find("LanternManager").GetComponent<LanternManager>();
        container = GameObject.Find("Container").transform;
        parentTransform = transform.parent;
        isPlugged = true;
        isReplugged = false;
    }

    // Update is called once per frame
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

    private void PickUp()
    {
        tempPosition = transform.localPosition;
        equipped = true;
        slotFull = true;
        transform.SetParent(container);
        transform.localPosition = new Vector3(0, -0.5f, 0);
        rigidBody.isKinematic = true;
    }

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

    private void PutBack()
    {
        Detach();
        transform.localPosition = tempPosition;
    }

    private void Detach()
    {
        equipped = false;
        slotFull = false;
        rigidBody.isKinematic = false;
        transform.SetParent(parentTransform);
    }
}
