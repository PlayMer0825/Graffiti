using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_This : MonoBehaviour
{
    public GameObject GameObject;
    public static bool position = false;
    public static bool Back_Front = false;
    public static bool Back_Back = false;
    public static bool City = false;

    public float A, B, C;
    public float E, F, G;
    public float H, I, J;
    public float K, L, M;

    // Start is called before the first frame update
    void Start()
    {
        if(position)
        {
            GameObject.transform.position = new Vector3(A,B,C);
            position = false;
            Telepoter_This.telepoter = false;
        }
        if(Back_Front)
        {
            GameObject.transform.position = new Vector3(E,F,G);
            Back_Front = false;
            Telepoter_This.telepoter = false;
        }
        if (Back_Back)
        {
            GameObject.transform.position = new Vector3(H,I,J);
            Back_Back = false;
            Telepoter_This.telepoter = false;
        }
        if(City)
        {
            GameObject.transform.position = new Vector3(K,L,M);
            City = false;
            Telepoter_This.telepoter = false;
        }
    }
}
