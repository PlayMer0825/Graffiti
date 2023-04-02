using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum EActorList
{
    Titi,
    Red,
    Blue,
    Yellow,
    NPC1
};

public class DialogLoad : MonoBehaviour
{
    public DialogSave dialogSave;

    // 이벤트 아이디 
    [HideInInspector]
    public string eventID_Setting;

    // 현재 불러온 이벤트를 담는 배열 
    public string[] dialogArray;

    public EActorList EactorList = EActorList.Titi;

    // 이벤트 ID와 액터 ID를 인자로 전달하면 해당 대화 정보를 반환.
    private string[] GetDialog(string eventID, string actorID)
    {
        Dictionary<string, List<string>> currentDialog;
        if (dialogSave.save.TryGetValue(eventID, out currentDialog))
        {
            List<string> dialogList = new List<string>();
            if (currentDialog.TryGetValue(actorID, out List<string> actorDialog))
            {
                // 해당 이벤트에 속한 해당 액터의 모든 대화 정보를 반환합니다.
                foreach (string context in actorDialog)
                {
                    dialogList.Add(context);
                }
            }

            return dialogList.ToArray();
        }

        // 해당 이벤트가 없거나 해당 액터의 대화 정보가 없으면 빈 배열을 반환.
        return new string[0];
    }

    private void Start()
    {
        dialogSave = FindObjectOfType<DialogSave>();
    }


    /// <summary>
    /// 이벤트 아이디를 설정할 수 있는 메서드 
    /// </summary>
    /// <param name="_eventID"></param>
    /// <returns></returns>
    public string SetEventID
    {
        get { return eventID_Setting; }
        set { eventID_Setting = value; }
    }

}
