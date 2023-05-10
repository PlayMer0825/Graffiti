using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 컷씬의 타입
/// </summary>
public enum ECutSceneType { Video, Image } 

[System.Serializable]
public class CCutSceneDataSet
{
    public ECutSceneType eCutSceneType;
    public Sprite[] cutSceneImages;
    public VideoClip videoClip;
}
public class CutSceneManager : MonoBehaviour
{
    [Header("컷씬 출력될 이미지들 넣어주기")]
    [SerializeField] Image cutSceneImg;
    [SerializeField] RawImage videoCutSceneImage;
    [SerializeField] private VideoPlayer videoPlayer;
    private VideoClip videoClip;

    [Header("컷씬 데이터 지정")]
    [SerializeField]
    private CCutSceneDataSet[] cutSceneDataSet;

    private bool isPlayingCutScene = false;
    int currentCutSceneIndex = 0; // 현재 재생되고 있는 컷씬 인덱스
    int currentImageindex = 0; // 이미지 재생 인덱스 

    [SerializeField] int NowCutSceneIndex = 0; // 
    

    private void Awake()
    {
        //cutSceneImg = GetComponent<Image>();
        //videoCutSceneImage = GetComponent<RawImage>();
        videoClip = GetComponent<VideoClip>();

        videoPlayer.clip = cutSceneDataSet[currentCutSceneIndex].videoClip; // videoPlayer 의 clip 에 할당 
        cutSceneImg.sprite = cutSceneDataSet[currentCutSceneIndex].cutSceneImages[0]; // Image 인덱스 초기화



    }

    private void Start()
    {
        //ClearCutScene();
        StartCutScene();
    }

    private void Update()
    {
        if (isPlayingCutScene == false) return;

        if (cutSceneDataSet[currentCutSceneIndex].eCutSceneType == ECutSceneType.Image)
        {
            if (currentCutSceneIndex >= cutSceneDataSet.Length - 1) // 모든 이미지를 출력한 경우
            {
                EndCutScene(); // 컷씬 종료
            }
            else
            {
                // 다음 이미지를 출력
                cutSceneImg.sprite = cutSceneDataSet[currentCutSceneIndex].cutSceneImages[0];
                currentCutSceneIndex++;
            }
        }

    }
    public void ClearCutScene()
    {
        isPlayingCutScene = false;
        videoCutSceneImage.gameObject.SetActive(false);
        cutSceneImg.gameObject.SetActive(false);
        currentCutSceneIndex = 0;

    }
    public void StartCutScene() // 공통적으로 들어가는 컷씬이 시작할 때 
    {
        // 컷씬 데이터가 없으면 리턴
        if (cutSceneDataSet == null || cutSceneDataSet.Length == 0) { Debug.Log("컷씬 데이터가 없습니다"); return; }
        // 이미 컷씬이 실행되고 있으면 리턴
        if (isPlayingCutScene == true) { Debug.Log("컷씬이 현재 실행중입니다."); return; }

        ECutSceneType type = cutSceneDataSet[currentCutSceneIndex].eCutSceneType;

        switch(type)
        {
            case ECutSceneType.Video:
                {
                    Debug.Log(currentCutSceneIndex + " 번째는 비디오다!");
                    VideoCutScene();
                }
                break;
            case ECutSceneType.Image:
                {
                    Debug.Log(currentCutSceneIndex + " 번째는 이미지다!");
                    ImageCutScene();
                }
                break;
        }
    }
    public void EndCutScene() // 컷씬이 끝날 때 다음 행동 처리 할 수 있는 느낌으로 
    {
        isPlayingCutScene = false;
        currentCutSceneIndex = 0;

        cutSceneImg.gameObject.SetActive(false);
        videoCutSceneImage.gameObject.SetActive(false);
    }

    public void VideoCutScene() // 비디오 컷씬에 대한 처리들 
    {
        Debug.Log("VideoCutScene 메서드 정상 작동중!");
        videoPlayer.clip = cutSceneDataSet[currentCutSceneIndex].videoClip;
        videoCutSceneImage.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    public void ImageCutScene() // 이미지 컷씬에 대한 처리들 
    {
        Debug.Log("ImageCutScene 메서드 정상 작동중!");
        cutSceneImg.gameObject.SetActive(true);
        //cutSceneImg.sprite = cutSceneDataSet[currentImgIndex].cutSceneImages[]
    }

}
