using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 
/// </summary>

public class LoadDialog : MonoBehaviour
{
    //public DialogLoad dialogLoad;
    public DialogSave dialogSave;
    public EventIDSetting eventIDSetting;


    public GameObject dialogObject;
    public TextMeshProUGUI dialogText;

    public string[] dialogArray;
    //[SerializeField] private string eventID_Setting;

    private int currentIndex = 0;

    void Awake()
    {
        eventIDSetting = GetComponent<EventIDSetting>();
        Init();
    } 

    public void Init()
    {
        dialogObject.SetActive(false); 
        dialogText.text = null;
    }

    void Update()
    {
        // getEventID 값을 읽어옴
        //currentEventID = eventID_Setting;

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if(currentIndex < dialogArray.Count)
        //    {
        //        dialogText.text = dialogArray[currentIndex];
        //        currentIndex++;
        //    }
        //    else
        //    {
        //        currentIndex = 0;
        //        ResetDialog();

        //    }
        //}
        //LoadDialogArray(eventIDSetting.SetEventID();
        GetInteractEventID(eventIDSetting.SetEventID());
    }

    //public void LoadDialogArray(string _eventID)
    //{
    //    dialogArray = DialogSave.Get_Dialog(dialogSave.save, _eventID).ToArray();
    //}

    // 초기화하는 메서드 
    private void ResetDialog()
    {
        dialogText.text = null;

        dialogObject.SetActive(false);
    }


    private void ShowDialog()
    {
        currentIndex = 0;
        dialogText.text = dialogArray[currentIndex];
        dialogObject.SetActive(true);
    }

    public void GetInteractEventID(string _eventID)
    {

        dialogArray = DialogSave.Get_Dialog(dialogSave.saveDialog, _eventID).ToArray();

    }

    //void SetEventID(string _eventID)
    //{
    //    eventID_Setting = _eventID;
    //}

    /// <summary>
    /// 이벤트를 할당해서 내용을 불러올 수 있음 -> 출력 자체도 dialogArray 의 내용들을 하나씩 상호작용을 할 때마다 불러오는 식으로 가능함
    /// </summary>
    //public void ExEventSetting(string _eventID)
    //{
    //    //dialogObject.transform.position = 
    //    eventID_Setting = ChangeEventID(_eventID);
    //    dialogText.text = dialogArray[0];
    //    dialogObject.SetActive(true);
    //}

    ///// <summary>
    ///// 이벤트 ID 를 설정해주는 메서드
    ///// </summary>
    ///// <param name="_currentEventID"></param>
    ///// <returns></returns>
    //string ChangeEventID(string _currentEventID)
    //{
    //    return _currentEventID;
    //}
}
