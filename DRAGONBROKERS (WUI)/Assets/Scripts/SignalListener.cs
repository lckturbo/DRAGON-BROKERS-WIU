using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    private void Enable()
    {
       signal.RegisterListener(this);
    }

    private void Disable()
    {
        signal.DeRegisterListener(this); 
    }
}
