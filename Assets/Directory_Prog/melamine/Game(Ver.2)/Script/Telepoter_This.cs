using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Telepoter_This : MonoBehaviour
{
    public Image image;
    public static bool telepoter = false;

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        
        SceneManager.LoadScene(target_SceneName);
    }


    private bool isPlayerTriggered = false;
    [SerializeField]
    private string target_SceneName = string.Empty;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0) && isPlayerTriggered)
        {
            PlayerMove_SIDE.isLoad = false;
            telepoter = true;
            StartCoroutine(FadeCoroutine());
            if (target_SceneName == string.Empty)
                return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTriggered = false;
        }
    }
}
