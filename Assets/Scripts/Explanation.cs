using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    public GameObject howToPlay;
    // Start is called before the first frame update
    void Start()
    {
        howToPlay.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Button()
    {
        Time.timeScale = 1.0f;
        howToPlay.SetActive(false);
    }
}
