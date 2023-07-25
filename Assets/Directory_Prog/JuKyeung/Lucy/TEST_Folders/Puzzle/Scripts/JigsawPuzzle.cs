using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPuzzle : MonoBehaviour
{
    public GameObject puzzlePosSet; // 퍼즐 위치를 포함하는 부모 오브젝트에 대한 참조
    public GameObject puzzlePieceSet; // 퍼즐 조각을 포함하는 부모 오브젝트에 대한 참조

    public bool IsClear()
    {
        for (int i = 0; i < puzzlePosSet.transform.childCount; i++)
        {
            if (puzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                Debug.Log("JigSawPuzzle.IsClear(): 퍼즐 조각이 이동했지만 올바른 위치에 배치되지 않았습니다.");
                return false;
            }

            else if (puzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<PuzzlePiece>().piece_no != i)
            {
                Debug.Log("JigSawPuzzle.IsClear(): 퍼즐 조각이 올바른 위치에 배치되지 않았습니다.");
                return false;
            }
        }
        Debug.Log("JigSawPuzzle.IsClear(): 퍼즐이 완성되었습니다!");

        return true;
    }
}
