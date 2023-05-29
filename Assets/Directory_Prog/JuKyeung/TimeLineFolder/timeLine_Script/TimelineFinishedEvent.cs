using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Events;
using UnityEngine.Playables;
using System;

[Serializable]
public class TimelineFinishedEvent : UnityEvent { }

public class TimeLineEvent : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private TimelineFinishedEvent _timelineFinishedEvent;
    private void Start()
    {
        _playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector playableDirector)
    {
        if (_timelineFinishedEvent != null)
        {
            _timelineFinishedEvent.Invoke();
        }
    }
}