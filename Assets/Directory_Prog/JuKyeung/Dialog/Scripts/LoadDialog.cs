using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadDialog : MonoBehaviour
{
    public DialogLoad dialogLoad;
    public TextMeshProUGUI dialogText;
    private int i = 0;

    void Start()
    {
        dialogLoad = GameObject.FindObjectOfType<DialogLoad>();
    }
    void Update()
    {
        // getEventID 값을 읽어옴
        string currentEventID = dialogLoad.eventID_Setting;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(i < dialogLoad.dialogArray.Length)
            {
                dialogText.text = dialogLoad.dialogArray[i];
                i++;
            }
            else
            {
                i = 0;
                ResetText();

            }
        }
    }

    // 초기화하는 메서드 
    private void ResetText()
    {
        dialogText.text = null;
        
        dialogText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 이런식으로 이벤트를 할당해서 내용을 불러올 수 있음 -> 출력 자체도 dialogArray 의 내용들을 하나씩 상호작용을 할 때마다 불러오는 식으로 가능함
    /// </summary>
    public void ExEventSetting(string _eventID)
    {
        dialogLoad.eventID_Setting = ChangeEventID(_eventID);
        dialogText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 이벤트 ID 를 설정해주는 메서드
    /// </summary>
    /// <param name="_currentEventID"></param>
    /// <returns></returns>
    string ChangeEventID(string _currentEventID)
    {
        return _currentEventID;
    }
}
