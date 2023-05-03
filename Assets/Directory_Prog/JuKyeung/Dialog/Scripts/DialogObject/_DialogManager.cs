using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Actors
{
    Titi,
    Npc1
}

[System.Serializable]
public class ActorSetting
{
    public Actors actors;
    public string actorIDSet;
    [SerializeField] public GameObject dialogUIpos;
}

public class _DialogManager : MonoBehaviour
{
    DialogLoad dialogLoad;

    [Header("다이얼로그 UI 세팅")]
    public GameObject dialogBubblefreb;     // 다이얼로그 프리팹 넣을 곳
    [SerializeField] public TextMeshProUGUI dialogText;

    [Header("다이얼로그 UI 포지션")]
    //public Transform[] actorPositions;
    [SerializeField] private ActorSetting[] actorSettings;
    private GameObject[] dialogs;

    [Header("현재 다이얼로그 설정")]
    [SerializeField] private int currentDialogIndex = 0;
    [SerializeField] private string currentEventID = "";
    private void Awake()
    {
        //dialogs = new GameObject[actorPositions.Length];
        dialogs = new GameObject[actorSettings.Length];
        dialogText = dialogBubblefreb.GetComponentInChildren<TextMeshProUGUI>();
        dialogLoad = GetComponent<DialogLoad>();

        InstantiateDialogObject();

    }

    private void Update()
    {
        for (int i = 0; i < actorSettings.Length; i++)
        {
            Vector3 dialogpos = actorSettings[i].dialogUIpos.transform.position;
            dialogs[i].transform.position = dialogpos;
        }

        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    SetDialogText(++currentDialogIndex);
        //}

    }

    void InstantiateDialogObject() // 초기 다이얼로그오브젝트 생성 ( 캐릭터의 위치 ) 
    {

        for (int i = 0; i < actorSettings.Length; i++)
        {
            Vector3 dialogPos = actorSettings[i].dialogUIpos.transform.position;
            dialogs[i] = Instantiate(dialogBubblefreb, dialogPos, Quaternion.identity, this.transform);
            dialogs[i].SetActive(true);
        }
    }

    void FindActor()
    {
        // dialogList 의 0을 시작으로 짝수번호와 ActorSetting 의 actorID 가 같다면은 그 actorSetting 에 있는 
        //dialogPos Object 를 Active 하면 되지 않을까
        // 대화가 끝났다. 즉 예를들어 스페이스를 두번 눌렀거나... 텍스트 효과가 끝났을떄 스페이스를 누르거나 했다면은 
    }

    ///


}
