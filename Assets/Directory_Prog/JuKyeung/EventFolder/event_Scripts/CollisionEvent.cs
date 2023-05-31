using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent CollisionStartEvent;
    private void OnTriggerEnter(Collider other)
    {
        CollisionStartEvent.Invoke();
    }
}
