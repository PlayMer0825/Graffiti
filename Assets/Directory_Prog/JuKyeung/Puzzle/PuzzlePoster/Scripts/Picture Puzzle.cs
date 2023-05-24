using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PicturePuzzle : MonoBehaviour
{
    [SerializeField] private int requiredPieceCount;
    [SerializeField] private List<PicturePiece> puzzlePieces; // 퍼즐 조각 리스트

    private int currentPieceCount = 0;
    private bool puzzleCompleted = false;

    public UnityEvent onPicturePuzzleCompleted; // 퍼즐 완료 이벤트

    private void Start()
    {
        // 퍼즐 조각 리스트 초기화
        puzzlePieces = new List<PicturePiece>(transform.GetComponentsInChildren<PicturePiece>());
    }

    public void IncrementPieceCount()
    {
        // 퍼즐 조각 개수 증가
        currentPieceCount++;

        // 현재 조각 개수가 필요한 개수와 같아지면 퍼즐 완료
        if (currentPieceCount == requiredPieceCount && !puzzleCompleted)
        {
            puzzleCompleted = true;
            onPicturePuzzleCompleted.Invoke(); // 완료 이벤트 호출
        }
    }

    public bool IsPuzzleCompleted()
    {
        return puzzleCompleted;
    }

    //public List<PicturePiece> GetPuzzlePieces()
    //{
    //    return PicturePiece;
    //}

    public int GetRequiredPieceCount()
    {
        return requiredPieceCount;
    }

    public void StartPicturePuzzle()
    {
        // 퍼즐이 시작되면 퍼즐 조각 드래그 가능하게 설정
        foreach (PicturePiece piece in puzzlePieces)
        {
            piece.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void ResetPicturePuzzle()
    {
        // 퍼즐 리셋
        currentPieceCount = 0;
        puzzleCompleted = false;
        foreach (PicturePiece piece in puzzlePieces)
        {
            piece.GetComponent<BoxCollider2D>().enabled = true;
            piece.transform.position = piece.GetStartPosition();
        }
    }
}
