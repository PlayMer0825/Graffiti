using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Actors
{
    Titi = 100,
    Red = 200,
    Yellow = 300,
    Blue = 400,
    NPC1 = 500
}


public class LoadDialog : MonoBehaviour
{
    public DialogLoad dialogLoad;

    public GameObject dialogObject;
    public TextMeshProUGUI dialogText;

    [SerializeField] string currentEventID;
    private int i = 0;

    void Awake()
    {
        dialogLoad = GameObject.FindObjectOfType<DialogLoad>();
        dialogObject.SetActive(false);
        dialogText.text = null;
    } 
    void Update()
    {
        // getEventID 값을 읽어옴
        currentEventID = dialogLoad.eventID_Setting;

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

        dialogObject.SetActive(false);
    }

    /// <summary>
    /// 이벤트를 할당해서 내용을 불러올 수 있음 -> 출력 자체도 dialogArray 의 내용들을 하나씩 상호작용을 할 때마다 불러오는 식으로 가능함
    /// </summary>
    public void ExEventSetting(string _eventID)
    {
        //dialogObject.transform.position = 
        dialogLoad.eventID_Setting = ChangeEventID(_eventID);
        dialogText.text = dialogLoad.dialogArray[0];
        dialogObject.SetActive(true);
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
