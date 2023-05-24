using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzleUI; // 퍼즐 UI
    public GameObject nextEvent; // 다음 이벤트 오브젝트

    private bool puzzleSolved = false;

    void Start()
    {
        puzzleUI.SetActive(false);
    }

    public void ActivatePuzzle()
    {
        puzzleUI.SetActive(true);
    }

    public void CheckPuzzleSolved()
    {
        // 퍼즐이 완성됐는지 검사하는 로직 (e.g., 모든 퍼즐 조각 위치가 올바른 위치에 있는지)

        puzzleSolved = true;

        if (puzzleSolved)
        {
            puzzleUI.SetActive(false);
            nextEvent.SetActive(true);
        }
    }
}
