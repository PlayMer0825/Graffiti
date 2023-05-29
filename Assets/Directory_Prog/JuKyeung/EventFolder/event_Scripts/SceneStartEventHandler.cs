using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneStartEventHandler : MonoBehaviour
{

    public UnityEvent SceneStartEvent;

    private void Start()
    {
        SceneStartEvent.Invoke();
    }
}
