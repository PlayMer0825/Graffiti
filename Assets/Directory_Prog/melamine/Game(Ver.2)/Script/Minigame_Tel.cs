using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Tel : MonoBehaviour
{
    public GameObject Riverside;
    public GameObject Minigame;

    public static bool isBoard = false;

    void Update()
    {
        if(isBoard==false)
        {
            Minigame.SetActive(true);
            Riverside.SetActive(false);
        }
        else if(isBoard==true)
        {
            Minigame.SetActive(false);
            Riverside.SetActive(true);
        }
    }
}
