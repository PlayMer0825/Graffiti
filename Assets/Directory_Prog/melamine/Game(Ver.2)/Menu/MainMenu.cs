//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource start;
    public AudioSource quit;
    public Image image;
    public static bool loadGame = false;
    void Start()
    {
        image.color = new Color(0, 0, 0, 0f);
        image.enabled = false;
        Position.position = false;
    }
    private bool isPlayerTriggered = false;
   public void NewGame()
    {
        start.Play();
        image.enabled = true;
        PlayerPrefs.DeleteAll();
        StartCoroutine(FadeCoroutine());
    }

    public void LoadGame()
    {
        start.Play();
        image.enabled = true;
        loadGame = true;
        DataManager.Instance.LoadGameData();
        StartCoroutine(FadeCoroutine1());
    }
    public void Option()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        quit.Play();
        Application.Quit();
    }

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator FadeCoroutine1()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        SceneManager.LoadScene(DataManager.Instance.data.sceneIndex);
    }
}
