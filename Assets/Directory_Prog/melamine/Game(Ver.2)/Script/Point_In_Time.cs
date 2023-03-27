using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_In_Time : MonoBehaviour
{
    public bool state = true;
    public bool area = false;
    public bool hideOut_Area = false;

    public static bool hello = true;
    // Start is called before the first frame update
    void Start()
    {
        state = true;
        area = false;
        hello = true;
        hideOut_Area = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
