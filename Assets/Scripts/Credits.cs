using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public GameObject backGround;
    public GameObject credit;
    public GameObject button;

    public bool isCredit = false;

    public float x;
    public float y;
    public float y_1;

    void Start()
    {
        backGround.SetActive(false);
        credit.SetActive(false);
        button.SetActive(true);

        isCredit = false;

        x= credit.transform.position.x;
        y = credit.transform.position.y;
        y_1= credit.transform.position.y;
    }
    void Update()
    {
        credit.transform.position = new Vector2(x, y);
        if (isCredit == true)
        {
            y++;
        }

        if (y >= 2160)
        {
            y = y_1;
            isCredit = false;
            backGround.SetActive(false);
            credit.SetActive(false);
            button.SetActive(true);
        }
    }

    public void PrintCredit()
    {
        backGround.SetActive(true);
        credit.SetActive(true);
        button.SetActive(false);
        isCredit = true;
    }

  
}

