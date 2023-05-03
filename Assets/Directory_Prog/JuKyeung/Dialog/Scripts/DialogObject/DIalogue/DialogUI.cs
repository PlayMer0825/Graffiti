using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using DG.DemiLib;

public class DialogUI : MonoBehaviour
{
    DialogLoad dialogLoad;
    [SerializeField] private TextMeshProUGUI dialogText;

    [SerializeField]
    private float typingSpeed = 0.01f;
    private int currentIndex = 0;

    private bool isTyping = false;
    private bool showAllText = false;

    private void Awake()
    {
        dialogLoad = GetComponent<DialogLoad>();
    }

    private void Update()
    {
        GetEvent();
    }

    public void GetEvent()
    {
        StopAllCoroutines();
        StartCoroutine(TypeDialog());
    }
    IEnumerator TypeDialog()
    {
        dialogText.text = "";
        currentIndex = 0;
        isTyping = true;

        while (currentIndex < dialogLoad.dialogArray.Length)
        {
            string line = dialogLoad.dialogArray[currentIndex];

            // If show all text flag is true, show entire line
            if (showAllText)
            {
                dialogText.text += line + "\n";
                currentIndex++;
            }
            else // Otherwise, show line one character at a time
            {
                for (int i = 0; i < line.Length; i++)
                {
                    dialogText.text += line[i];
                    yield return new WaitForSeconds(typingSpeed);
                }

                // Wait for player input to show next line
                while (!Input.GetKeyDown(KeyCode.Space))
                {
                    yield return null;
                }

                // Move to next line
                currentIndex++;
                dialogText.text += "\n";
            }
        }

        isTyping = false;
    }

    private void FixedUpdate()
    {
        if (isTyping)
        {
            // Show entire line if player holds down space bar
            if (Input.GetKey(KeyCode.Space))
            {
                showAllText = true;
            }
            else
            {
                showAllText = false;
            }
        }
    }


}
