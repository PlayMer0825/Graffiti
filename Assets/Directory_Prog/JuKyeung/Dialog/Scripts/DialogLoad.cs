using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogLoad : MonoBehaviour
{
    // 저장된 대화 정보를 가져올 DialogSave 스크립트의 인스턴스
    public DialogSave dialogSave;

    [SerializeField] string eventID;

    // 이벤트 ID와 액터 ID를 인자로 전달하면 해당 대화 정보를 반환합니다.
    public string[] GetDialog(string eventID)
    {
        Dictionary<string, List<string>> currentDialog;
        if (dialogSave.save.TryGetValue(eventID, out currentDialog))
        {
            List<string> dialogList = new List<string>();
            // 해당 이벤트에 속한 모든 대화 정보를 반환합니다.

            foreach (KeyValuePair<string, List<string>> pair in currentDialog)
            {
                string actorID = pair.Key;
                foreach (string context in pair.Value)
                {
                    dialogList.Add(actorID + " | " + context);
                }
            }

            return dialogList.ToArray();
        }

        // 해당 이벤트가 없으면 빈 배열을 반환합니다.
        return new string[0];
    }

    private void Start()
    {
        // DialogSave 클래스 인스턴스를 할당합니다.
        dialogSave = FindObjectOfType<DialogSave>();

        // eventID에 해당하는 대화 정보를 가져와서 개행 문자로 구분된 문자열로 출력합니다.
        //Debug.Log(string.Join("\n", GetDialog(eventID)));

        PrintDialog();

    }

    private void PrintDialog()
    {
        for (int i = 0; i < GetDialog(eventID).Length; i++)
        {
            Debug.Log(GetDialog(eventID)[i]);
        }
    }
}
