using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    public Image tutorialImage;
    public Sprite[] tutorialSprites;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;
    private int currentTutorialIndex = 0;
    private bool isUIActive = true;

    private void Start()
    {
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
    }

    private void Update()
    {
        if (isUIActive && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                ShowPreviousTutorialImage();
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                ShowNextTutorialImage();
        }
    }

    private void UpdateButtonInteractivity()
    {
        prevButton.interactable = currentTutorialIndex > 0;
        nextButton.interactable = currentTutorialIndex < tutorialSprites.Length - 1;
    }

    private void ShowNextTutorialImage()
    {
        currentTutorialIndex++;
        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity();

        if (currentTutorialIndex >= tutorialSprites.Length - 1)
        {
            exitButton.gameObject.SetActive(true);
        }
    }

    private void ShowPreviousTutorialImage()
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
        gameObject.SetActive(false);
    }

    public void SetUIActive(bool active)
    {
        isUIActive = active;
    }
}
