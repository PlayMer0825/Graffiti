using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{
    private HashSet<GameObject> interactibles = new HashSet<GameObject>();

    public UnityEvent<GameObject> EventTriggerEnter { get; private set; }
    public UnityEvent<GameObject> EventTriggerExit { get; private set; }

    private void Awake()
    {
        EventTriggerEnter = new UnityEvent<GameObject>();
        EventTriggerExit = new UnityEvent<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!interactibles.Contains(other.gameObject))
        {
            interactibles.Add(other.gameObject);
            EventTriggerEnter.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactibles.Contains(other.gameObject))
        {
            interactibles.Remove(other.gameObject);
            EventTriggerExit.Invoke(other.gameObject);
        }
    }

    public bool IsInTrigger(GameObject gameObject)
    {
        return interactibles.Contains(gameObject);
    }
}
