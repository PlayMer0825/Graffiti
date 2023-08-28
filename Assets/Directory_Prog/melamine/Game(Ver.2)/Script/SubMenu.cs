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
            if(subMenu.activeSelf==false)
            {
                subMenu.SetActive(true);
                save.SetActive(false);
                Time.timeScale = 0f;
            }
            else if(subMenu.activeSelf==true)
            {
                Time.timeScale = 1f;
                subMenu.SetActive(false);
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
        StartCoroutine(WaitForSecondsRealtime(3f));
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

    IEnumerator WaitForSecondsRealtime(double time)
    {
        save.SetActive(true);
        var startTime = System.DateTime.Now;
        var elapsedTime = 0d;
        while (elapsedTime < time)
        {
            yield return null;
            var currentTime = System.DateTime.Now;
            elapsedTime = (currentTime - startTime).TotalSeconds;
        }
        save.SetActive(false);
    }
}
