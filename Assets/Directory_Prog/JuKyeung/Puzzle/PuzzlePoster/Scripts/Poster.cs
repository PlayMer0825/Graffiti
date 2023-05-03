using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PosterMode
{
    BackgroundSelection,
    SloganSelection,
    complete
}

public class Poster : MonoBehaviour
{
    [SerializeField] private Image choiceBGImage;
    [SerializeField] private TextMeshProUGUI choiceSloganText;

    [Header("꾸미기 목록")]
    public Sprite[] bgImageLists;
    public TextMeshProUGUI[] sloganList;

    [Header("미리보기 포스터")]
    public Image preViewImg;
    private Sprite preViewSprite;
    public TextMeshProUGUI preViewSlogan;

    [Header("이동버튼")]
    public Button nextButton;
    public Button prevButton;

    private int currentBgImageIndex = 0;
    private int currentSloganTextIndex = 0;

    [Header("현재 포스터 작성 모드")]
    [SerializeField] private PosterMode currentMode;

    private void Start()
    {
        UpdateBackgroundImage();
        currentMode = PosterMode.BackgroundSelection;

    }

    public void ChangeMode(PosterMode posterMode)
    {
        currentMode = posterMode;
    }


    /// <summary>
    /// ">" 다음 버튼에 대한 처리
    /// </summary>
    public void NextButton()
    {
        if (currentMode == PosterMode.BackgroundSelection)
        {
            currentBgImageIndex++;
            if (currentBgImageIndex >= bgImageLists.Length)
            {
                currentBgImageIndex = 0;
            }
            UpdateBackgroundImage();
        }
        else if (currentMode == PosterMode.SloganSelection)
        {
            currentSloganTextIndex++;
            if (currentSloganTextIndex >= sloganList.Length)
            {
                currentSloganTextIndex = 0;
            }
            UpdateSloganText();
        }
    }

    /// <summary>
    /// "<"이전버튼에 대한 처리
    /// </summary>
    public void PrevButton()
    {
        // 배경
        if (currentMode == PosterMode.BackgroundSelection)
        {
            currentBgImageIndex--;
            if (currentBgImageIndex < 0)
            {
                currentBgImageIndex = bgImageLists.Length - 1;
            }
            UpdateBackgroundImage();
        }
        // 슬로건 
        else if (currentMode == PosterMode.SloganSelection)
        {
            currentSloganTextIndex--;
            if (currentSloganTextIndex < 0)
            {
                currentSloganTextIndex = sloganList.Length - 1;
            }
            UpdateSloganText();
        }
    }

    /// <summary>
    /// 배경 이미지의 업데이트 
    /// </summary>
    private void UpdateBackgroundImage()
    {
        choiceBGImage.sprite = bgImageLists[currentBgImageIndex];
    }

    private void UpdateSloganText()
    {
        //choiceSloganText = sloganList[currentSloganTextIndex];
        //choiceSloganText.gameObject.SetActive(true);

        for (int i = 0; i < sloganList.Length; i++)
        {
            if (i == currentSloganTextIndex)
            {
                sloganList[i].gameObject.SetActive(true);
                Debug.Log("ㅇㅇ");
            }
            else
            {
                sloganList[i].gameObject.SetActive(false);
                Debug.Log("xx");
            }
        }
    }


    /// <summary>
    /// 확정 버튼에 대한 처리
    /// </summary>
    public void ConfirmedButton()
    {
        if (currentMode == PosterMode.BackgroundSelection)
        {
            preViewImg.sprite = bgImageLists[currentBgImageIndex];
            ChangeMode(PosterMode.SloganSelection);
        }
        else if (currentMode == PosterMode.SloganSelection)
        {
            preViewSlogan = sloganList[currentSloganTextIndex];
            choiceSloganText.gameObject.SetActive(false);
            ChangeMode(PosterMode.complete);
        }
    }

    /// <summary>
    /// 포스터퍼즐 - 배경선택 모드
    /// </summary>
    public void SetBackgroundMode()
    {
        ChangeMode(PosterMode.BackgroundSelection);
    }

    /// <summary>
    /// 포스터 퍼즐 - 문구선택 모드
    /// </summary>
    public void SetTextMode()
    {
        ChangeMode(PosterMode.SloganSelection);

    }


    public void PreviewImage()
    {

    }
}
