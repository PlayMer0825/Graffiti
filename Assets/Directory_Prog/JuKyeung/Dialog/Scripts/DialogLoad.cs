using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogLoad : MonoBehaviour
{
    // 저장된 대화 정보를 가져올 DialogSave 스크립트의 인스턴스
    public DialogSave dialogSave;

    public string eventID_Setting;

    public string[] dialogArray;

    // 이벤트 ID와 액터 ID를 인자로 전달하면 해당 대화 정보를 반환합니다.
    public string[] GetDialog(string _eventID)
    {
        Dictionary<string, List<string>> _currentDialog;
        if (dialogSave.save.TryGetValue(_eventID, out _currentDialog))
        {
            List<string> dialogList = new List<string>();
            List<string> actorList = new List<string>();
            // 해당 이벤트에 속한 모든 대화 정보를 반환합니다.

            foreach (KeyValuePair<string, List<string>> pair in _currentDialog)
            {
                string _actorID = pair.Key;
                foreach (string _context in pair.Value)
                {
                    // 임시로 ActorID 도 함께 확인하기 위해 같이 가져옴. 
                    dialogList.Add(/*_actorID + " | " +*/ _context);
                    actorList.Add(_actorID);
                    //dialogList.Add(_context);
                }
            }

            return dialogList.ToArray();
        }

        // 해당 이벤트가 없으면 빈 배열을 반환합니다.
        return new string[0];
    }

    private void Awake()
    {
        dialogSave = FindObjectOfType<DialogSave>();
    }

    private void Update()
    {
        LoadDialog();
    }

    /// <summary>
    /// 일단 불러올 것(디버그) 
    /// </summary>
    public void LoadDialog()
    {
        dialogArray = GetDialog(eventID_Setting);
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