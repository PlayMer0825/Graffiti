using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pages : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [NonSerialized] public int currentPageIndex = 0;  // 현재 페이지 인덱스

    public PuzzleManager puzzleManager;

    public void ShowNextPage()
    {
        pages[currentPageIndex].SetActive(false);  // 현재 페이지 비활성화
        currentPageIndex++;
        if (currentPageIndex >= pages.Length)
            currentPageIndex = 0;

        pages[currentPageIndex].SetActive(true);  // 다음 페이지 활성화
        puzzleManager.SetActivePageIndex(currentPageIndex);
    }

    public void ShowPreviousPage()
    {
        pages[currentPageIndex].SetActive(false);  // 현재 페이지 비활성화
        currentPageIndex--;
        if (currentPageIndex < 0)
            currentPageIndex = pages.Length - 1;

        pages[currentPageIndex].SetActive(true);  // 이전 페이지 활성화
        puzzleManager.SetActivePageIndex(currentPageIndex);
    }
}
