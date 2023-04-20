using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [Header("상호작용 세팅")]
    public GameObject interactUIprefabs;
    public Transform[] interact_UIPos;
    private GameObject[] uiObjs;
    [SerializeField] private KeyCode interactSetKey;

    [Header("체크")]
    [SerializeField] bool isPlayerInCheck = false;

    private void Awake()
    {
        isPlayerInCheck = false;

        uiObjs = new GameObject[interact_UIPos.Length];       
        for(int i =0; i< interact_UIPos.Length; i++)
        {
            Vector3 _interactUIpos = interact_UIPos[i].position;
            uiObjs[i] = Instantiate(interactUIprefabs, _interactUIpos, Quaternion.identity, transform);
            uiObjs[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerInCheck = true;
            interactUIprefabs.SetActive(true);
            StartInteract(interactSetKey);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isPlayerInCheck = false;
    }

    void StartInteract(KeyCode _keyCode)
    {
        if(Input.GetKeyDown(_keyCode))
        {

        }
    }
    //    [SerializeField] private GameObject interact_UI;
    //    [SerializeField] private GameObject interact_UIUpPos;

    //    [SerializeField] Camera lookCamera;

    //    [SerializeField] DialogUI dialogUI;

    //    [Header("플레이어 인아웃 체크")]
    //    [SerializeField] bool playerCheck;

    //    private void Awake()
    //    {
    //        playerCheck = false;
    //    }


    //    private void OnTriggerEnter(Collider other)
    //    {
    //        if (other.tag == "Player")
    //        {
    //            playerCheck = true;
    //        }
    //    }

    //    private void OnTriggerExit(Collider other)
    //    {
    //        playerCheck = false;
    //    }

    //    private void Update()
    //    {
    //        DialogPosition();
    //        PlayerInOutCheck();

    //    }
    //    private void PlayerInOutCheck()
    //    {
    //        if (playerCheck == true)
    //        {
    //            Debug.Log("플레이어 IN");
    //            interact_UI.SetActive(true);

    //            if (Input.GetKeyDown(KeyCode.E))
    //            {
    //                Debug.Log("E 키의 입력이 감지되었습니다. ");
    //                interact_UI.SetActive(false);
    //                dialogUI.GetContext();
    //            }

    //        }
    //        else if (playerCheck == false)
    //        {
    //            Debug.Log("플레이어 Out");
    //            interact_UI.SetActive(false);
    //        }
    //    }

    //    void DialogPosition() // 카메라를 바라보아라
    //    {
    //        interact_UI.transform.position = interact_UIUpPos.transform.position;
    //        interact_UI.transform.LookAt(lookCamera.transform);
    //    }
}
