using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion_ActiongControl : MonoBehaviour
{
    [SerializeField] private Animator anim;
    // private AnimationClip clip =
    private bool animEnd;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetDialogAction(string _actioneventName)
    {

        if (anim != null)
        {
            anim.SetBool(_actioneventName, true);
            animEnd = false;
        }
        else
        {
            Debug.Log("ㄴㄴㄴ");
        }

    }

    private void Update()
    {
        
    }

    private void StopAnimation(string _actioneventName)
    {
        anim.SetBool(_actioneventName, false);
    }

}
