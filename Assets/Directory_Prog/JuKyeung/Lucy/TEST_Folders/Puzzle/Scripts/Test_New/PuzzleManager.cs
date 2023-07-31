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
        Street,
        Building,
        Note
    }

    [Serializable]
    public class PuzzlePiece
    {
        [Header("퍼즐 조각 오브젝트")]
        public GameObject pieceObject;
        [Header("정답 오브젝트 ")]
        public GameObject correctAnswer;
        [Header("스냅 거리 조절")]
        public float snapDistance; // 조각을 스냅시키기 위한 거리
        [Header("퍼즐 조각이 정답 위치에 위치했을 때 이벤트")]
        public UnityEvent puzzleSolvedEvent;
    }

    public int totalPieces;
    [Header("퍼즐 설정")]
    public List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();

    [Header("퍼즐을 전부 정답 위치에 스냅시켰을 때 일어날 이벤트")]
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
            if(puzzleType == PuzzleType.Note)
            {
                puzzleComplete = true;
                endPuzzleEvent.Invoke();
            }
            else if(puzzleType == PuzzleType.Street)
            {
                puzzleComplete = true;
                endPuzzleEvent.Invoke();
            }  
            
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
                puzzlePiece.puzzleSolvedEvent.Invoke();
            }
            else
            {
                Draggable draggable = piece.GetComponent<Draggable>();
                draggable.draggableTransform.anchoredPosition = draggable.originalTransform;
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
                puzzlePiece.puzzleSolvedEvent.Invoke();
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
