using Insomnia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    public GameObject subMenu;
    public GameObject save;

    private void Start()
    {
        subMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && DrawManager.Instance.IsDrawing == false)
        {
            subMenu.SetActive(true);
            save.SetActive(false);
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
        save.SetActive(true);
        //SetSave();
        DataManager.Instance.SaveGameData();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene/MainMenu");
    }

    public void SetSave()
    {
        StartCoroutine(Save_Complete());
    }

    IEnumerator Save_Complete()
    {
        Invoke("Invoke_save", 1.0f);
        yield return new WaitForSecondsRealtime(1f);
    }
public void Invoke_save()
    {
        save.SetActive(false);
    }
}
