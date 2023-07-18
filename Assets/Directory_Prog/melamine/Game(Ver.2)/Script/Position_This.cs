using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_This : MonoBehaviour
{
    public GameObject GameObject;
    public static bool position = false;
    public static bool Front = false;
    public static bool Back = false;
    public static bool City = false;
    public static bool Secret = false;
    public static bool Riverside = false;

    public float A, B, C;
    public float E, F, G;
    public float H, I, J;
    public float K, L, M;
    public float Q, R, S;

    // Start is called before the first frame update
    void Start()
    {
        if (Secret)
        {
            Debug.Log("Secret");
            GameObject.transform.position = new Vector3(A,B,C);
            position = false;
            Telepoter_This.telepoter = false;
        }
        if(Front)
        {
            Debug.Log("Front");
            GameObject.transform.position = new Vector3(E,F,G);
            Front = false;
            Telepoter_This.telepoter = false;
        }
        if (Back)
        {
            Debug.Log("Back");
            GameObject.transform.position = new Vector3(H,I,J);
            Back = false;
            Telepoter_This.telepoter = false;
        }
        if(City)
        {
            Debug.Log("city");
            GameObject.transform.position = new Vector3(K,L,M);
            City = false;
            Telepoter_This.telepoter = false;
        }
        if (Riverside)
        {
            Debug.Log("river");
            GameObject.transform.position = new Vector3(Q, R, S);
            Riverside = false;
            Telepoter_This.telepoter = false;
        }
    }

    public static void setFalse()
    {
        position = false;
        Front = false;
        Back = false;
        City = false;
        Secret = false;
        Riverside = false;
    }
}
