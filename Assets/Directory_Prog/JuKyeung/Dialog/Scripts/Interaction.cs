using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class Interaction : MonoBehaviour
{
    //public LoadDialog loadDialog;

    //[Header("상호작용 세팅")]
    //public GameObject interactUIprefabs;
    //public Transform[] interact_UIPos;
    //private GameObject[] uiObjs;

    //public GameObject[] interactArea;


    //[Header("체크")]
    //[SerializeField] bool isPlayerInCheck = false;

    //private void Awake()
    //{
    //    isPlayerInCheck = false;

    //    // 상호작용 대상 오브젝트에 UI 추가 
    //    uiObjs = new GameObject[interact_UIPos.Length];       
    //    for(int i =0; i< interact_UIPos.Length; i++)
    //    {
    //        Vector3 _interactUIpos = interact_UIPos[i].position;
    //        uiObjs[i] = Instantiate(interactUIprefabs, _interactUIpos, Quaternion.identity, transform);
    //        uiObjs[i].SetActive(false);
    //    }

    //    BoxCollider interactAreaObj;

    //    foreach(GameObject interact in interactArea)
    //    {
    //        interactAreaObj= interact.gameObject.GetComponent<BoxCollider>();
    //    }
    //}

    //private void Update()
    //{
    //    if(isPlayerInCheck && Input.GetKeyDown(KeyCode.F))
    //    {
    //        foreach(GameObject interact in interactArea)
    //        {
    //            Debug.Log("상호작용가능" + interact.name);
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        isPlayerInCheck = true;

    //        foreach(GameObject interact in interactArea)
    //        {
    //            if(Input.GetKeyDown(KeyCode.T))
    //            {
    //                interactUIprefabs.SetActive(true);
    //                loadDialog.GetInteractEventID();
    //            }
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    isPlayerInCheck = false;

    //    foreach(GameObject interact in interactArea)
    //    {
    //        interactUIprefabs.SetActive(false);
    //    }
    //}

    public LoadDialog loadDialog;

    [SerializeField] private GameObject interact_UI;
    [SerializeField] private GameObject interact_UIUpPos;

    //[SerializeField] Camera lookCamera;

    //[SerializeField] DialogUI dialogUI;

    [Header("플레이어 인아웃 체크")]
    [SerializeField] bool playerCheck;

    private void Awake()
    {
        playerCheck = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerCheck = false;
    }

    private void Update()
    {
        //DialogPosition();
        PlayerInOutCheck();

    }
    private void PlayerInOutCheck()
    {
        if (playerCheck == true)
        {
            Debug.Log("플레이어 IN");
            interact_UI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("T 키의 입력이 감지되었습니다. ");
                interact_UI.SetActive(false);
                //dialogUI.GetContext();
                //loadDialog.GetInteractEventID();
            }

        }
        else if (playerCheck == false)
        {
            Debug.Log("플레이어 Out");
            interact_UI.SetActive(false);
           // loadDialog.GetInteractEventID() ;
        } 
    }

    //void DialogPosition() // 카메라를 바라보아라
    //{
    //    interact_UI.transform.position = interact_UIUpPos.transform.position;
    //    interact_UI.transform.LookAt(lookCamera.transform);
    //}
}
