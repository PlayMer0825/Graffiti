using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Onetime_ButtonController : MonoBehaviour
{
    public Button button;
    public PlayableDirector timeline;
    
    public bool isTimelinePlayed = false;

    private void Start()
    {
        button.onClick.AddListener(OnClickHandler);
    }

    private void OnClickHandler()
    {
        
    }

    public void Chapter1_FirstGraffitiBtn_FirstClick()
    {

    }
}
