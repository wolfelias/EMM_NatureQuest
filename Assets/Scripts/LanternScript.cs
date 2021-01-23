using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    public PlugScript plugScript;
    public Sprite lanternOff;
    public Sprite lanternOn;

    // Update is called once per frame
    void Update()
    {
        if (plugScript.isPlugged)
        {
            this.GetComponent<SpriteRenderer>().sprite = lanternOn;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = lanternOff;
        }
    }
}
