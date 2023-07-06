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

    private bool isTimelinePlayed = false;


    private void Start()
    {
        // 타임라인 재생 완료 이벤트 수신을 위해 함수 연결
        timeline.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // 타임라인 재생 완료 이벤트 호출
        timelineFinishedEvent.Invoke();
        Debug.Log(timeline.name + "에 등록된 이베트들 실행");

        // 타임라인 재생 상태 변경
        isTimelinePlayed = true;
    }

    public void PlayTimeline()
    {
        if (!isTimelinePlayed)
        {
            timeline.Play();
        }
    }
}
