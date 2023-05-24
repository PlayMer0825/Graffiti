using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleInteract : MonoBehaviour
{
    [SerializeField] private int targetLayer; // Layer 로 해서 모든 퍼즐에 적용하기 

    private PicturePuzzle PicturePuzzle;
    // 

    private void OnTriggerEnter(Collider other)
    {
        // 퍼즐 레이어를 가지고 있는 오브젝트와 충돌했다면 
        if (other.gameObject.layer == targetLayer)
        {
            if (other.TryGetComponent(out PicturePuzzle))
            {
                PicturePuzzle.StartPicturePuzzle();
            }
        }
    }

    private void StartPicturePuzzle()
    {
        //unityEvent.Invoke();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

}

