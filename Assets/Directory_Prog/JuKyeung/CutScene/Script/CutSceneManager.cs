using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 컷씬의 타입
/// </summary>
public enum ECutSceneType { Video, Image } 

public enum ECutSceneNames
{
    CutScene01,
    CutScene02,
    CutScene03
}

[System.Serializable]
public class CCutSceneData
{
    public ECutSceneType eCutSceneType;
    public Image[] cutSceneImages;
    public RawImage videoCutSceneImage;
    public VideoClip videoClip;

    public void ECutSceneType(ECutSceneType _cutSceneType, Image[] _cutSceneImages = null, RawImage _videoCutSceneImage = null, VideoClip _cutVideoClip = null)
    {
        this.eCutSceneType = _cutSceneType;
        this.cutSceneImages = _cutSceneImages;
        this.videoCutSceneImage = _videoCutSceneImage;
        this.videoClip = _cutVideoClip;
    }
}
public class CutSceneManager : MonoBehaviour
{
    public Image image;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    public CCutSceneData[] cutSceneData;
    private int index = 0;

    private bool isPlayingCutScene;
    private void Start()
    {
        SetCutSceneType(index);
    }

    private void Update()
    {
        if(isPlayingCutScene)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SetCutSceneType(index);
                index++;
            }
        }
    }
    public void SetCutSceneType(int _index) // 컷씬 지정
    {
        ECutSceneType type = cutSceneData[_index].eCutSceneType;

        if (type == ECutSceneType.Video) /// 컷씬 타입이 Video 라면
        {
            VideoClip videoClip = cutSceneData[_index].videoClip;  // CCutSceneData 의 videoClip 과 같음 
            rawImage.gameObject.SetActive(true);
            image.gameObject.SetActive(false);

            PlayVideo();
            videoPlayer.loopPointReached += VideoCutSceneCheckOver; // 비디오 컷씬 끝났을 때 컷씬 종료 


        }
        else if (type == ECutSceneType.Image) /// 컷씬 타입이 Image 라면 
        {
            Image[] images = cutSceneData[_index].cutSceneImages; // CCutSceneData 의 cutSceneImages 
            
        }

    }   

    void PlayVideo() // 비디오 컷씬 시작 
    {
        if(videoPlayer != null && videoPlayer.isPrepared)
        {
            isPlayingCutScene = true;
            videoPlayer.Play();
        }
    }

    void PlayImgCutScene()
    {

    }
    void VideoCutSceneCheckOver(VideoPlayer _vp) // 비디오 컷씬 종료 
    {
        Debug.Log("Video 컷씬 종료");
        if(videoPlayer!=null && videoPlayer.isPrepared)
        {
            videoPlayer.Stop();
            isPlayingCutScene = false;
            rawImage.gameObject.SetActive(false);
        }

    }
    void ImageCutSceneCheckOver(Image _img) // 이미지 컷씬 종료 
    {
        Debug.Log("Image 컷씬 종료");
        isPlayingCutScene = false;
        image.gameObject.SetActive(false);
    }
}
