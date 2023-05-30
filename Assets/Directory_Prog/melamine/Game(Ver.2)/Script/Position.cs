using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public GameObject GameObject;
    public static bool position = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!position)
        {
            GameObject.transform.position = new Vector3(-15f, 1.0f, -10.0f);
        }
        else if(position)
        {
            GameObject.transform.position = new Vector3(345f, 1.0f, -10.0f);
            position = false;
        }
    }
}
