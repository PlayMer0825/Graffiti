//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Insomnia;

public class MainMenu : MonoBehaviour
{
    public AudioSource start;
    public AudioSource quit;
    public Image image;
    private Credits m_credits = null;

    public static bool loadGame = false;
    void Start()
    {
        image.color = new Color(0, 0, 0, 0f);
        image.enabled = false;
        Position.position = false;
        m_credits = GameObject.FindObjectOfType<Credits>();

    }
    private bool isPlayerTriggered = false;
   public void NewGame()
    {
        start.Play();
        image.enabled = true;
        PlayerPrefs.DeleteAll();
        Minigame_Tel.isBoard = false;

        if(QuestContainer.Instance != null)
            QuestContainer.Instance.RemoveAllQuest();

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

    public void PrintCredit()
    {

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

    public  void OnClickCreditStart()
    {
        if (null == m_credits)
            return;

        m_credits.PrintCredit();
    }

    public void OnClickCreditSkip()
    {
        if (null == m_credits)
            return;

        m_credits.PrintCredit();
    }
}
