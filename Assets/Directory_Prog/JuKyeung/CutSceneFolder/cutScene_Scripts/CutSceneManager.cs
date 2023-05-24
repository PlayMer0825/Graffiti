using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Events;


public class CutSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 비디오 컴포넌트
    public Button skipButton; // 스킵 버튼
    [SerializeField] float skipDuratioin = 3.0f;
    private bool isPlaying;
    [SerializeField] private GameObject cutSceneObject;

    public VideoClip[] cutScenes; // 비디오 컷씬들을 저장하는 배열
    private int currentSceneIndex = 0; // 현재 컷씬 인덱스

    public UnityEvent cutSceneFinishEvent; // 컷씬 끝난 후 이벤트

    private void Start()
    {
        if (cutSceneObject == null)
        {
            Debug.LogError("CutScene 오브젝트 미할당");
        }
        else
        {
            cutSceneObject.SetActive(false);
            PlayCutScene(currentSceneIndex); // 첫 번째 컷씬 재생

            videoPlayer.loopPointReached += OnCompletionReached;
        }

    }

    void OnCompletionReached(VideoPlayer vp)
    {
        currentSceneIndex++;

    }

    private void Update()
    {
        if (!isPlaying) return;

        if (videoPlayer.isPlaying)
        {
            return;
        }


        if (currentSceneIndex < cutScenes.Length) // 배열 다 안끝났으면 컷씬 진행 
        {

            PlayCutScene(currentSceneIndex);
        }
        else // 다 끝났으면 종료
        {
            HandleAllCutScenesFinished();
        }

    }

    private void PlayCutScene(int index)
    {
        isPlaying = true;
        cutSceneObject.SetActive(true);
        videoPlayer.clip = cutScenes[index];
        videoPlayer.Play();

        // 일정 시간 후에 스킵 버튼 활성화
        float duration = skipDuratioin;
        Invoke("EnableSkipButton", duration);


    }

    private void EnableSkipButton()
    {
        skipButton.gameObject.SetActive(true);
        skipButton.interactable = true;
    }

    private void DisableSkipButton()
    {
        skipButton.gameObject.SetActive(false);
        skipButton.interactable = false;
    }

    public void SkipCutScene()
    {
        videoPlayer.Stop();
        skipButton.interactable = false;
        currentSceneIndex++;

        if (currentSceneIndex < cutScenes.Length)
        {
            PlayCutScene(currentSceneIndex);
        }
        else
        {
            // 모든 컷씬 종료 후에 이벤트 처리
            HandleAllCutScenesFinished();
        }
    }

    private void HandleAllCutScenesFinished()
    {
        // 컷씬 종료 이벤트 처리
        Debug.Log("컷씬 완전 다 끝남");
        cutSceneFinishEvent.AddListener(DisableSkipButton); // 버튼 닫기... 
        cutSceneObject.SetActive(false);
        cutSceneFinishEvent.Invoke();
    }
}
