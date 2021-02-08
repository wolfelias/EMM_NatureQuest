using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file FloatingTextHandler.cs
 *
 *  @brief A script used for individualising the floating text effect
 *
 *  @author Sunan Regi Maunakea
 */
public class FloatingTextHandler : MonoBehaviour
{
    public float x = 0;
    public float y = 25;

    /*! @brief Start method of the script
     *  
     *  Destroy the floating text 2 seconds after instantiation
     */
    void Start()
    {
        Destroy(gameObject, 2f);
        transform.localPosition += new Vector3(x, y, 0);
    }
}
