using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Collider))]


public class Teleporter : MonoBehaviour
{
    public Image image;

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 1;
        while (fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerTriggered)
        {
            StartCoroutine(FadeCoroutine());
            if (target_SceneName ==  string.Empty)
                return;
            SceneManager.LoadScene(target_SceneName);
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
