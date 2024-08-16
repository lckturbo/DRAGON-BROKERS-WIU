using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalGame signalgame;
    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    private void Enable()
    {
        signalgame.RegisterListener(this);
    }

    private void Disable()
    {
        signalgame.DeRegisterListener(this);
    }
}
