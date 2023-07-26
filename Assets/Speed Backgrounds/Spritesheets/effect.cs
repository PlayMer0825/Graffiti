using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    public static Animator animator1;
    public GameObject gameObject;
    public static bool isEffect = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        isEffect = false;
        animator1= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isEffect)
        {
            Invoke("OnInvoke_Effect", 0f);
        }
        else if(!isEffect)
        {
            gameObject.SetActive(false);
        }
    }

    void OnInvoke_Effect()
    {
        gameObject.SetActive(true);
    }

}
