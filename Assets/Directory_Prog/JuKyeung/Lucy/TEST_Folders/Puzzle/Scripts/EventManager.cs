using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public delegate void PuzzleEventHandle();

    [Header("이벤트 시작할 때")]
    public UnityEvent StartPuzzleEvent; 
    [Header("이벤트 하는 중")]
    public UnityEvent InProgressPuzzleEvent;
    [Header("이벤트가 끝나면")]
    public UnityEvent EndPuzzleEvent;


    [Header("오브젝트들")]
    [SerializeField] private GameObject example_pictureObject;
    private void Start()
    {
        Transform objects = transform.Find("ExamplePicture");

        if(objects != null )
        {
            example_pictureObject = objects.gameObject;
        }
    }

    private void StartEventSignal() { Debug.Log("이벤트 시작 신호 전송"); }

    // 이벤트 시작 시 호출되는 함수
    public void StartEvent()
    {
        Debug.Log("이벤트가 시작되었습니다.");
        example_pictureObject.gameObject.SetActive(true);

        StartPuzzleEvent.Invoke();
         
        // 이벤트 진행 중을 나타내는 신호 보내기
        EventInProgress();
    }
    


    // 이벤트 진행 중을 나타내는 신호를 보내는 함수
    public void EventInProgress()
    {
        Debug.Log("이벤트가 진행 중입니다.");
        InProgressPuzzleEvent.Invoke();

        // 이벤트가 끝났음을 알리는 함수 호출
        EndEvent();
    }

    // 이벤트 종료 후 호출되는 함수
    public void EndEvent()
    {
        Debug.Log("이벤트가 종료되었습니다.");

        // 종료 후에 신호를 보내는 함수 호출
        SignalAfterEvent();
    }

    // 이벤트 종료 후에 신호를 보내는 함수
    private void SignalAfterEvent()
    {
        EndPuzzleEvent.Invoke();
        Debug.Log("이벤트 종료 후 신호를 보냈습니다.");
    }
}
