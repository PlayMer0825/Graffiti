using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int targerLayer; // 16 번 레이어
     
    private HavePicture havePicture;
    private noteBook _noteBook;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == targerLayer)
        {
            if (other.TryGetComponent(out havePicture)) // 상호작용한 대상 오브젝트가 16번 레이어일 경우 HavePicture 스크립트를 가져옴 
            {
                // 가져와서 ? 
            }
        }
    }
}
