using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent CollisionStartEvent;


    [Header("이동시킬 오브젝트(거의 대부분 플레이어) 가 있을 때")]
    public Transform targetObject;
    public Vector3 newPosition;
    public Quaternion newRotation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            CollisionStartEvent.Invoke();

        }
    }

    public void MoveObject()
    {
        targetObject.position = newPosition;
        targetObject.rotation = newRotation;
    }


}
