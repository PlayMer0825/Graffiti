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

    private void Start()
    {
        // 타임라인 재생 완료 이벤트 수신을 위해 함수 연결
        timeline.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // 타임라인 재생 완료 이벤트 호출
        timelineFinishedEvent.Invoke();
    }


}
