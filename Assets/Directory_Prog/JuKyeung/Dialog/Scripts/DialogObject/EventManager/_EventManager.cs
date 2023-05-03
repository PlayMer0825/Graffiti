using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Event : UnityEvent { } 

public class _EventManager : MonoBehaviour
{
    public int eventTriggerValue = 0;
    public int currentCount = 0;
    public bool eventActive = false;

    public Event onEventTriggered;

    public void TriggerEvent()
    {
        eventActive = true;
        onEventTriggered.Invoke();

    }

    public void EndEvent()
    {
        eventActive = false;
        currentCount = 0;

    }

    private void CheckEventCondition()
    {
        if (currentCount >= eventTriggerValue)
        {
            TriggerEvent();
        }
    }
    private void Update()
    {
        if (eventActive)
        {
            // 이벤트 실행 중인 경우
            onEventTriggered.Invoke(); // 등록된 이벤트 함수 실행
        }
        else
        {
            // 이벤트 실행 중이 아닌 경우
            CheckEventCondition();
        }

        if (currentCount >= eventTriggerValue)
        {
            EndEvent();
        }
    }

}
