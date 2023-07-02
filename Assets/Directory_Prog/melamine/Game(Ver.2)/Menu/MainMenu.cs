using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool loadGame = false;
    void Start()
    {
        Position.position = false;
    }
    private bool isPlayerTriggered = false;
   public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        loadGame = true;
        DataManager.Instance.LoadGameData();
        SceneManager.LoadScene(DataManager.Instance.data.sceneIndex);
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
        Application.Quit();
    }
}
