using Insomnia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    
    public GameObject subMenu;

    private void Start()
    {
        subMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&DrawManager.Instance.IsDrawing==false)
        {
            subMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

   public void Resume()
    {
        Time.timeScale = 1f;
        subMenu.SetActive(false);
    }

    public void Save()
    {
        DataManager.Instance.SaveGameData();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene/MainMenu");
    }
}
