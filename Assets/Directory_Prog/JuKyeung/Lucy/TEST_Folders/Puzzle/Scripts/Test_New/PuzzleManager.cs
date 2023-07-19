using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class PuzzleManager : MonoBehaviour
{
    public enum PuzzleType
    {
        Normal,
        Note
    }

    [Serializable]
    public class PuzzlePiece
    {
        public GameObject pieceObject;
        public GameObject correctAnswer;
        public float snapDistance; // 조각을 스냅시키기 위한 거리
        public UnityEvent onCorrectPlacement;
    }

    public int totalPieces;
    public List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();

    public UnityEvent endPuzzleEvent;

    private int correctPieces;
    private int activePageIndex; // 현재 활성화된 페이지 인덱스
    private bool puzzleComplete;

    public PuzzleType puzzleType;

    private void Start()
    {
        correctPieces = 0;
        activePageIndex = 0;
        puzzleComplete = false;
    }

    private void Update()
    {
        if (!puzzleComplete && correctPieces == totalPieces)
        {
            puzzleComplete = true;
            endPuzzleEvent.Invoke();
        }
    }

    public void SetActivePageIndex(int index)
    {
        activePageIndex = index;
    }

    public void OnPieceDragged(GameObject piece)
    {
        PuzzlePiece puzzlePiece = GetPuzzlePieceByGameObject(piece);

        if(puzzleType == PuzzleType.Note)
        {
            // 현재 페이지가 활성화된 페이지인지 확인
            if (IsPieceInCorrectPosition(piece, puzzlePiece) && activePageIndex == puzzlePieces.IndexOf(puzzlePiece))
            {
                Draggable draggable = piece.GetComponent<Draggable>();
                draggable.enabled = false;
                draggable.transform.SetParent(puzzlePiece.correctAnswer.transform, false);
                draggable.transform.localPosition = Vector3.zero;

                correctPieces++;
                puzzlePiece.onCorrectPlacement.Invoke();
            }
        }
        else
        {
            if (IsPieceInCorrectPosition(piece, puzzlePiece))
            {
                Draggable draggable = piece.GetComponent<Draggable>();
                draggable.enabled = false;
                draggable.transform.SetParent(puzzlePiece.correctAnswer.transform, false);
                draggable.transform.localPosition = Vector3.zero;

                correctPieces++;
                puzzlePiece.onCorrectPlacement.Invoke();
            }

        }


    }

    private bool IsPieceInCorrectPosition(GameObject piece, PuzzlePiece puzzlePiece)
    {
        float distance = Vector3.Distance(piece.transform.position, puzzlePiece.correctAnswer.transform.position);
        return distance <= puzzlePiece.snapDistance;
    }

    private PuzzlePiece GetPuzzlePieceByGameObject(GameObject piece)
    {
        return puzzlePieces.Find(p => p.pieceObject == piece);
    }



}
