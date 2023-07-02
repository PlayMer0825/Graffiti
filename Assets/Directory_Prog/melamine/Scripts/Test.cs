using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //DataManager.Instance.LoadGameData();
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }
}
