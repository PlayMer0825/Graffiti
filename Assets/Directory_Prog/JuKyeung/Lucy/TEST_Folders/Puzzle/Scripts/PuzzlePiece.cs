using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int snapOffset = 60; // 퍼즐 조각을 위치에 맞추기 위한 오프셋 거리
    public JigsawPuzzle puzzle; // JigsawPuzzle 스크립트에 대한 참조
    public int piece_no; // 퍼즐 조각을 나타내는 번호

    void Awake()
    {
        puzzle = FindObjectOfType<JigsawPuzzle>(); 
    }

    void Start()
    {
        piece_no = gameObject.name[gameObject.name.Length - 1] - '0'; 
    }

    void Update()
    {
        //...

    }

    bool CheckSnapPuzzle()
    {

        for (int i = 0; i < puzzle.puzzlePosSet.transform.childCount; i++)
        {
            //위치에 자식오브젝트가 있으면 이미 퍼즐조각이 놓여진 것입니다.
            if (puzzle.puzzlePosSet.transform.GetChild(i).childCount != 0)
            {
                continue;
            }
            else if (Vector2.Distance(puzzle.puzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
            {
                if(piece_no == i)
                {
                    transform.SetParent(puzzle.puzzlePosSet.transform.GetChild(i).transform);
                    transform.localPosition = Vector3.zero;
                    return true;
                }
            }
        }
        return false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // 드래그하는 동안 퍼즐 조각의 위치를 업데이트합니다.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //일치하는 위치가 없을 경우 부모자식 관계를 해제합니다.
        if (!CheckSnapPuzzle())
        {
            transform.SetParent(puzzle.puzzlePieceSet.transform);
        }

        if (puzzle.IsClear())
        {
            Debug.Log("완전~ Clear");
        }

    }

}
