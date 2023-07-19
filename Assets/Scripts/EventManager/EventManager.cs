using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    public static EventManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<EventManager>();
                if(instance == null)
                {
                    GameObject obj = new GameObject("EventManager");


                    instance = obj.AddComponent<EventManager>();
                }
            }
            return instance;
        }
    }

    private Dictionary<string, UnityEvent> evnetDictionary;

}
