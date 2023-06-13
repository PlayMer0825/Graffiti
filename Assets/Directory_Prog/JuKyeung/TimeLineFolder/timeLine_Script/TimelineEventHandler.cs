using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Events;


public class TimelineEventHandler : MonoBehaviour
{
    public PlayableDirector timeline;
    public UnityEvent timelineFinishedEvent;
    public GameObject targetObject;

    private void Start()
    {
        // 타임라인 재생 완료 이벤트 수신을 위해 함수 연결
        timeline.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // 타임라인 재생 완료 이벤트 호출
        timelineFinishedEvent.Invoke();

        // 타임라인이 끝날 때의 현재 위치로 오브젝트의 위치 설정
        Vector3 currentPosition = targetObject.transform.position;
        targetObject.transform.position = currentPosition;
    }

    public void SetPositionObject(GameObject objectToMove, Vector3 newPosition)
    {
        // 오브젝트의 위치 변경
        objectToMove.transform.position = newPosition;
    }

    public void SetTargetObject(GameObject _targetObject)
    {
        targetObject = _targetObject;
    }
}
