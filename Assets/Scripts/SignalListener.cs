using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;


    /**
    *   call the event
    */
   public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    /**
    *   add THIS SignalListener to the Listener-List in Signal
    */
    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    /**
    *   remove THIS SignalListener from Listener-List in Signal
    */
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}
