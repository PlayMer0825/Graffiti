using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialog : MonoBehaviour
{
    private DialogLoad dialogLoad;
    void Start()
    {
        dialogLoad = GameObject.FindObjectOfType<DialogLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        // getEventID 값을 읽어와서 사용
        string getEventID = dialogLoad.eventID_Setting;
    }

    /// <summary>
    /// 이런식으로 이벤트를 할당해서 내용을 불러올 수 있음 -> 출력 자체도 dialogArray 의 내용들을 하나씩 상호작용을 할 때마다 불러오는 식으로 가능함
    /// </summary>
    public void Event101()
    {
        dialogLoad.eventID_Setting = ChangeEventID("100101");
    }
    public void Event401()
    {

        dialogLoad.eventID_Setting = ChangeEventID("100401"); // 
    }
    

    string ChangeEventID(string _currentEventID)
    {
        return _currentEventID;
    }
}
