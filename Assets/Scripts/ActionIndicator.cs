using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIndicator : MonoBehaviour
{
    
    /**
    *   Signal Speech Bubble on top of player head
    */
    public GameObject actionIndicator;
    private bool isActive = false;
    
    /**
    *   set the Speech Bubble to active or not active
    */
    public void ChangeIndicator()
    {
        isActive = !isActive;
        if(isActive)
        {
            actionIndicator.SetActive(true);
        } else
        {
            actionIndicator.SetActive(false);
        }
    }
}
