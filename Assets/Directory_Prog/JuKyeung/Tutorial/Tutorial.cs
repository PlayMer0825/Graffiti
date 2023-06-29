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

    public PlayerMove_SIDE playerMoveSide ;

    [SerializeField] private bool isGraffitiActive = false;

    private void OnEnable()
    {

        currentTutorialIndex = 0;


        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();

        if (tutorialSprites.Length <= 1)
        {
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
        }
        else
        {
            prevButton.gameObject.SetActive(currentTutorialIndex > 0);
            exitButton.gameObject.SetActive(false);

        }

        if(currentTutorialIndex <= tutorialSprites.Length -1)
        {
            nextButton.gameObject.SetActive(true);
        }
        isUIActive = true;

    }

    //private void OnDisable()
    //{

    //    currentTutorialIndex = 0;
    //}

    private void Start()
    {
        DisplayTutorialImage(currentTutorialIndex);

        isGraffitiActive = false;

        playerMoveSide = FindObjectOfType<PlayerMove_SIDE>();
    }

    private void Update()
    {
        if (isUIActive  /*&& (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))*/)
        {
            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //    ShowPreviousTutorialImage();
            //else if (Input.GetKeyDown(KeyCode.RightArrow))
            //    ShowNextTutorialImage();

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

    public void OnPrevButtonClicked()
    {
        ShowPreviousTutorialImage();
    }

    public void OnExitButtonClicked()
    {
        // 튜토리얼 완료 이벤트 호출
        onTutorialCompleted?.Invoke();

        gameObject.SetActive(false);

        isUIActive = false;

        if(isGraffitiActive == true)
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
