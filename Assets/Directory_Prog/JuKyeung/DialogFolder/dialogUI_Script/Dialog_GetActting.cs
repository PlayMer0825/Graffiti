using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_GetActting : MonoBehaviour
{
    public string get_AnimationName =  "";

    public AnimationClip clip;
    private Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void SetAinmation()
    {
        animation.clip = clip;
    }


}
