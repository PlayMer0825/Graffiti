using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private bool isChanging = false; // 전환중인가
    private string targetSceneName; // 전환 씬 이름


    public void ChangeScene(string _sceneName)
    {
        if (isChanging)
        {
            Debug.Log("씬 전환중임");
            return;
        }

        targetSceneName = _sceneName;
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        isChanging = true;

        yield return SceneManager.LoadSceneAsync(targetSceneName);

        isChanging = false;
    }
}
