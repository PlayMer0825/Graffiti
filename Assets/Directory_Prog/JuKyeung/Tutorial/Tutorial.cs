using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public Image tutorialImage;
    public Sprite[] tutorialSprites;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private int currentTutorialIndex = 0;
    private int index = 0;
    [SerializeField] private bool isUIActive = true;

    [System.Serializable]
    public class TutorialCompletedEvent : UnityEvent { }
    public TutorialCompletedEvent onTutorialCompleted;

    public PlayerMove_SIDE playerMoveSide;

    [SerializeField] private bool isGraffitiActive = false;

    private void OnEnable()
    {
        currentTutorialIndex = 0;

        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();

        if (tutorialSprites.Length <= 1) // 켜질 때 스프라이트 갯수가 1보다 작거나 같으면 아무런 버튼도 Active 하지 않음 
        {
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
        }
        else // 그게 아니라면 일단 
        {
            prevButton.gameObject.SetActive(currentTutorialIndex > 0); // currentTutorialIndex 번호가 0보다 큰 상태라면 prevButton 을 활성화 

            exitButton.gameObject.SetActive(false);

        }

        isUIActive = true;

    }


    private void Start()
    {
        DisplayTutorialImage(currentTutorialIndex); // 디스플레이 이미지 초기화

        isGraffitiActive = false;

        playerMoveSide = FindObjectOfType<PlayerMove_SIDE>();
    }

    private void Update()
    {
        if (isUIActive)
        {
            if (currentTutorialIndex > 0)
            {
                prevButton.gameObject.SetActive(true);
            }

            else
            {
                prevButton.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);
            }

            playerMoveSide.enabled = false;

        }
    }

    private void UpdateButtonInteractivity()
    {
        prevButton.interactable = currentTutorialIndex > 0;
        nextButton.interactable = currentTutorialIndex < tutorialSprites.Length - 1;
    }

    private void ShowNextTutorialImage() // 다음 이미지 
    {
        currentTutorialIndex++;
        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();

        if (currentTutorialIndex >= tutorialSprites.Length - 1)
        {
            exitButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
    }

    private void ShowPreviousTutorialImage() // 뒤로 이미지 
    {
        currentTutorialIndex--;
        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();
        exitButton.gameObject.SetActive(false);
    }

    private void DisplayTutorialImage(int index)
    {
        tutorialImage.sprite = tutorialSprites[index];
    }

    public void OnNextButtonClicked()
    {
        ShowNextTutorialImage();
    }

    public void OnPrevButtonClicked() // 어째서 작동을 안하지? 
    {
        ShowPreviousTutorialImage();
    }

    public void OnExitButtonClicked()
    {
        // 튜토리얼 완료 이벤트 호출
        onTutorialCompleted?.Invoke();

        gameObject.SetActive(false);

        isUIActive = false;

        if (isGraffitiActive == true) // 그래피티 상태라면은 
        {
            playerMoveSide.enabled = false;
            isGraffitiActive = false;
        }
        else
        {
            playerMoveSide.enabled = true;
        }

    }

    public void SetGraffitiActive(bool active)
    {
        isGraffitiActive = active;
    }

    //private void OnGraffitiButtonClicked()
    //{
    //    SetGraffitiActive(true);
    //}

}