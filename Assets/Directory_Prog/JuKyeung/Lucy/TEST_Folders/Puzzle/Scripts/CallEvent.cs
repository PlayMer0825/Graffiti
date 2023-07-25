using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEvent : MonoBehaviour
{
    public EventManager eventManager;

    public void GetEvent()
    {
        eventManager.StartEvent();
    }
}
