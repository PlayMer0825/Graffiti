using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PuzzleEffect : MonoBehaviour
{
    public Image xEffect;

    public GameObject moveObject;
    public Transform targetPosition;

    /// <summary>
    ///  점등 효과
    /// </summary>
    public void BlinkAnim()
    {
        StartCoroutine(BlinkAnimCorutine());
        Debug.Log("코루틴 실행");
    }
    public IEnumerator BlinkAnimCorutine()
    {
        Debug.Log("코루틴 실행구문");
        float duration = 2.0f; // 깜빡이는 전체 시간 (초)
        float blinkInterval = 0.5f; // 깜빡이는 주기 (초)

        float timer = 0f;
        while (timer < duration)
        {
            float alpha = Mathf.PingPong(timer / blinkInterval, 1f); // 0~1 사이로 반복되는 값
            Color color = xEffect.color;
            color.a = alpha;
            xEffect.color = color;

            yield return null;
            timer += Time.deltaTime;
        }

        // 마지막에 확실히 보이도록 설정
        Color finalColor = xEffect.color;
        finalColor.a = 1.0f;
        xEffect.color = finalColor;

        yield return 2.0f;
        Debug.Log("2초 지연");
        StartCoroutine( MoveObjectCorutine());
    }

    /// <summary>
    /// 특정 오브젝트를 타겟오브젝트의 위치로 이동시키는 효과
    /// </summary>
    //public void MoveObject()
    //{
    //    MoveObjectCorutine();
    //    Debug.Log("오브젝트 이동");
    //}

    private IEnumerator MoveObjectCorutine()
    {
        Debug.Log("오브젝트 이동 실행");
        // float moveDuration = 2.5f;
        float moveSpeed = 100f;


        //while(moveObject.transform.position != targetPosition.position)
        //{
        //    moveObject.GetComponent<Draggable>().enabled = false;
        //    moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, 
        //        targetPosition.position, moveSpeed * Time.deltaTime);

        //    yield return null;
        //}
        while (Vector3.Distance(moveObject.transform.position, targetPosition.position) > 0.1f)
        {
            moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position,
                targetPosition.position, moveSpeed * Time.deltaTime);

            yield return null;
        }
        moveObject.SetActive(false);

    }
}

