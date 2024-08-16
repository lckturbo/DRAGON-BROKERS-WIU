using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalGame signal;
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
