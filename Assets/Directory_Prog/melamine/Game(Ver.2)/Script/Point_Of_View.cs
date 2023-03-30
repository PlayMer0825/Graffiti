using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Point_Of_View : MonoBehaviour
{
    public void SavePlayer()
    {
        SaveData save=new SaveData();
        save.X=transform.position.x;
        save.Y=transform.position.y;
        save.Z=transform.position.z;
        SaveManager.Save(save);
    }

    public void LoadPlayer()
    {
        SaveData save = SaveManager.Load();
        transform.position=new Vector3(save.X, save.Y, save.Z);
    }
    public bool Side = true;
    public bool Tps = false;

    [SerializeField]
    public CinemachineVirtualCamera SIDE;

    [SerializeField]
    public CinemachineVirtualCamera TPS;

    [SerializeField]
    public InteractionTrigger trigger;

    [SerializeField]
    public Transform grabPosition;

    private GrabableObject grabbing;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Side = true;
        Tps = false;
        SIDE.enabled = true;
        TPS.enabled = false;
        grabbing = null;

        trigger.EventTriggerExit.AddListener(TriggerExit);
        animator = GetComponent<Animator>();

        animator.SetBool("isTps", false);

        //Cursor.visible= false;
        Debug.Log("1");
        SetResolution();
        Debug.Log("2");
    }
    public void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;
        Screen.SetResolution(setWidth, setHeight, true);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Side == true)
                    Tps_View();
                else if (Tps == true)
                    Side_View();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hit, 100.0f))
            {
                if (trigger.IsInTrigger(hit.collider.gameObject))
                {
                    if (hit.collider.TryGetComponent(out Changer changer))
                    {
                        GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = false;
                        GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = false;
                        GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = true;
                        SIDE.enabled = false;
                        TPS.enabled = true;
                        changer.MouseClicked();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (grabbing == null)
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay, out RaycastHit hit, 100.0f))
                {
                    if (trigger.IsInTrigger(hit.collider.gameObject))
                    {
                        if (hit.collider.TryGetComponent(out GrabableObject grabableObject))
                        {
                            if (!grabableObject.IsGrab)
                            {
                                grabbing = grabableObject;
                                grabableObject.Grab();
                            }
                        }
                    }
                }
            }
            else
            {
                grabbing.Put();
                grabbing = null;
            }
        }
        if (GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().hideOut_Area == true
            && GameObject.Find("Changer").GetComponent<Changer>().Hide_Check==false)
        {
            GameObject.Find("Changer").GetComponent<Changer>().Hide_Check = true;
            animator.SetBool("isTps", true);
            GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = true;
            SIDE.enabled = false;
            TPS.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (grabbing != null)
        {
            grabbing.FollowOnFixedTime(grabPosition, transform.rotation);
        }
    }

    private void TriggerExit(GameObject gameObject)
    {
        //if (gameObject.TryGetComponent(out Changer changer))
        //{
        //    GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = true;
        //    SIDE.enabled = true;
        //    TPS.enabled = false;
        //    changer.Exit();
        //}
    }
    void Side_View()
    {
        animator.SetBool("isTps", false);
        Side = true;
        Tps = false;
        SIDE.enabled = true;
        TPS.enabled = false;
        gameObject.GetComponent<PlayerMove_SIDE>().enabled = true;
        gameObject.GetComponent<PlayerMove_TPS>().enabled = false;
    }

    void Tps_View()
    {
        animator.SetBool("isTps", true);
        Side = false;
        Tps = true;
        SIDE.enabled = false;
        TPS.enabled = true;
        gameObject.GetComponent<PlayerMove_SIDE>().enabled = false;
        gameObject.GetComponent<PlayerMove_TPS>().enabled = true;
    }
}
