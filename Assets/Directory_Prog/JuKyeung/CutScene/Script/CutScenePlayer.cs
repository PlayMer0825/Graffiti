using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutScenePlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public VideoClip[] videoClip;

    private int currentClipIndex;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        currentClipIndex = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayNextClip();
        }
    }

    void PlayNextClip()
    {
        currentClipIndex++;
        if (currentClipIndex >= videoClip.Length)
        {
            currentClipIndex = 0;
        }

        videoPlayer.clip = videoClip[currentClipIndex];
        videoPlayer.Play();
    }

}
