using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /**
    *   Signal which holds SignalListeners  
    */
[CreateAssetMenu]
public class Signal : ScriptableObject
{
    /**
    *   List of all SignalListeners
    */
    public List<SignalListener> listeners = new List<SignalListener>();

    /**
    *   go through all listeners backwards and call the OnSignalRaised function
    */
    public void Raise()
    {
        for (int i = listeners.Count-1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    /**
    *   @param listener SignalListener that needs to be added to the list 
    */
    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    /**
    *   @param listener SignalListener that needs to be removed from the list  
    */
    public void DeRegisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }

}
