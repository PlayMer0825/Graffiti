using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineScript : MonoBehaviour
{
    public PlayerMove_SIDE playerMoveSide;

    private void Start()
    {
        playerMoveSide = FindObjectOfType<PlayerMove_SIDE>();
    }
    public void TimeLinePlaying()
    {
        playerMoveSide.enabled = false;
        Debug.Log("타임라인 플레잉");

    }
}
