using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 저장된 다이얼로그 가져오고 
/// </summary>
public class DialogLoad : MonoBehaviour
{
    public DialogSave dialogSave;
    public EventIDSetting eventIDSetting;

    public string[] dialogArray;

    void Awake()
    {
        eventIDSetting = GetComponent<EventIDSetting>();
        //Init();
    }

    private void Update()
    {
        GetInteractEventID(eventIDSetting.SetEventID());

    }
    //public void Init() // 초기화 다이얼로그의 내용을? 
    //{
    //    dialogObject.SetActive(false);
    //    dialogText.text = null;
    //}

    public void GetInteractEventID(string _eventID)
    {
        dialogArray = DialogSave.Get_Dialog(dialogSave.saveDialog, _eventID).ToArray();
    }

    public string[] ReturnDialogArray()
    {
        return dialogArray;
    }


}