using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorial : MonoBehaviour
{
    public Image tutorialImage; 
    public Sprite[] tutorialSprites; 
    public Button prevButton; 
    public Button nextButton; 

    private int currentTutorialIndex = 0; 

    private void Start()
    {
        DisplayTutorialImage(currentTutorialIndex); 
        UpdateButtonInteractivity(); 
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
    }

    private void ShowPreviousTutorialImage()
    {
        currentTutorialIndex--; 
        DisplayTutorialImage(currentTutorialIndex);
        UpdateButtonInteractivity(); 
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
}
