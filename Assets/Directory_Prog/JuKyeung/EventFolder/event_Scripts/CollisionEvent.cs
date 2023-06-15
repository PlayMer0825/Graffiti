using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent CollisionStartEvent;

    [Header("이동시킬 오브젝트(거의 대부분 플레이어) 가 있을 때")]
    public Transform targetObject;
    public Vector3 localOffset;  // 부모 오브젝트로부터의 상대적인 오프셋
    public Quaternion localRotationOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollisionStartEvent.Invoke();
        }
    }

    public void MoveObject()
    {
        // 부모 오브젝트의 로컬 좌표계를 기준으로 위치 변경
        targetObject.localPosition = localOffset;
        targetObject.localRotation = localRotationOffset;  // 부모 오브젝트의 회전에 영향받지 않도록 항상 초기 회전값을 할당
    }


}
