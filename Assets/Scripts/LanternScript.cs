using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file LanternScript.cs
 *
 *  @brief A script used for the lantern
 *
 *  @author Sunan Regi Maunakea
 *
 *  A class for each lantern, show the lantern on sprite if the plug is plugged
 *  to an outlet. If the plug is unplugged, show the lantern off sprite.
 */
public class LanternScript : MonoBehaviour
{
    public PlugScript plugScript;
    public Sprite lanternOff;
    public Sprite lanternOn;

    /*! @brief Update method of the script
     *  
     *  Check every frame whether the lantern plug is plugged
     */
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
