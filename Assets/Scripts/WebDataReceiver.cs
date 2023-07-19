using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebDataReceiver : MonoBehaviour
{
    public void Receive(string data)
    {
        if (string.IsNullOrEmpty(data)) { return; }

        Debug.Log("Receive => " + data);
    }
}
