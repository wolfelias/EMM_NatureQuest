using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIndicator : MonoBehaviour
{
    public GameObject actionIndicator;
    private bool isActive = false;
    
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
