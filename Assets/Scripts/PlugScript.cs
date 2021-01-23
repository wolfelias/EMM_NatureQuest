using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugScript : MonoBehaviour
{
    public bool equipped;
    public static bool slotFull;
    public Rigidbody2D rigidBody;

    private bool isNearOutlet;
    private bool isPickable;
    public bool isPlugged;

    private Transform container;
    private Transform plugs;
    private Vector3 outletPosition;

    // Start is called before the first frame update
    void Start()
    {
        container = GameObject.Find("Container").transform;
        plugs = GameObject.Find("plug").transform;
        isPlugged = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!equipped && Input.GetKeyDown(KeyCode.E) && !slotFull && isPickable)
            PickUp();
        if (equipped && Input.GetKeyDown(KeyCode.Q))
            Drop();
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
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;
        transform.SetParent(container);
        transform.localPosition = new Vector3(0, -0.5f, 0);
        rigidBody.isKinematic = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;
        rigidBody.isKinematic = false;

        if (isNearOutlet)
        {
            transform.SetParent(plugs);
            transform.position = outletPosition;
            isPlugged = true;
        }
        else
        {
            transform.SetParent(plugs);
            isPlugged = false;
        }
    }
}
