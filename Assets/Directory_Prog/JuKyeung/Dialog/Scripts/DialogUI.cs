using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using DG.DemiLib;

public class DialogUI : MonoBehaviour
{
    [Header("다이얼로그 UI 설정")]
    public GameObject dialog_bubble; // 다이얼로그 오브젝트
    [SerializeField] TextMeshProUGUI dialog_Context = null; // 다이얼로그 텍스트 UI

    [Header("Actor 들의 다이얼로그 위치")]
    public GameObject titi_Dialog_HeadUpPos; // 다이얼로그 위치 포지션

    [Header("UI 대상 카메라")]
    [SerializeField] Camera lookCamera;

    [SerializeField] bool playerCheck ;

    //string dialog_FileName; // csv_Dialoog 의 파일 이름
    //[SerializeField] private TextAsset csv_Dialog = null;

    //List<Dictionary<string, object>> dialog_Data;

    [Header("현재 이벤트 ID")]
    public int nowEventID;

    int nowIndex = 0;

    private void Awake()
    {
        dialog_bubble.SetActive(false);
        //dialog_FileName = csv_Dialog.name;
        //dialog_Data = CSVParser.Read(dialog_FileName);
    }

    private void Update()
    {
        DialogPosition();
    }

    public void GetContext()  // TextMeshProUGUI nowText
    {
        dialog_bubble.SetActive(true);
        //dialog_Context.text = dialog_Data[nowIndex]["Context"].ToString();

        if (Input.GetKeyDown(KeyCode.E))
        {
            //dialog_Context.text = dialog_Data[nowIndex]["Context"].ToString();
            TMProUGUIDoText.DoText(dialog_Context, 30f);
            Debug.Log(dialog_Context.text);
            nowIndex++;
        }
        
    }

    /// <summary>
    /// 다이얼로그는 카메라를 항상 바라보도록 설정되어 있습니다. 
    /// </summary>
    void DialogPosition()
    {
        dialog_bubble.transform.position = titi_Dialog_HeadUpPos.transform.position;
        dialog_bubble.transform.LookAt(lookCamera.transform);
    }

    /// <summary>
    /// 현재 이벤트 목록의 actorID 에 따라 출력되는 다이얼로그 박스의 위치를 찾아옵니다. 
    /// </summary>
    public void FindActorPos()
    {
        // 선택된 액터 ID에 맞는 GameObject를 찾아옵니다.
        GameObject actorObject = actorDialogPos.FirstOrDefault(x => x.name.Contains(EactorList.ToString()));

        // 액터 GameObject의 자식 중 다이얼로그 박스 위치를 나타내는 "DialogPos" 태그를 가진 오브젝트를 찾아옵니다.
        Transform dialogPosition = actorObject.transform.Find("DialogPos");

        // 다이얼로그 박스 위치를 dialogPosition 위치로 이동합니다.
        dialog_bubble.transform.position = dialogPosition.position;

        // 다이얼로그 박스가 카메라를 바라보도록 합니다.
        dialog_bubble.transform.LookAt(lookCamera.transform);
    }


    /// <summary>
    /// richText(다이얼로그의 색상 코드 등 )를 배제하기 위한 메서드
    /// </summary>
    /// <param name="richText"></param>
    /// <returns></returns>
    public float TextLenght(string richText)
    {
        float len = 0;
        bool inTag = false;

        foreach (var ch in richText)
        {
            if (ch == '<')
            {
                inTag = true;
                continue;
            }
            else if (ch == '>')
            {
                inTag = false;
            }
            else if (!inTag)
            {
                len++;
            }
        }
        Debug.Log(len);
        return len;
    }
}

public static class TMProUGUIDoText
{
    public static void DoText(this TextMeshProUGUI _text, float _duration)
    {
        _text.maxVisibleCharacters = 0;
        DOTween.To(x => _text.maxVisibleCharacters = (int)x, 0f, _text.text.Length, _duration / _text.text.Length);
    }
}
