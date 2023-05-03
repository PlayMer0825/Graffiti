using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
        }

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        sceneLoad.allowSceneActivation = false;
    }
}
