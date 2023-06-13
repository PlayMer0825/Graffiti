using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk_Sound : MonoBehaviour
{
    public AudioSource run;
    public AudioClip walkSound;

    
    public void PlayWalkSound()
    {
        run.PlayOneShot(walkSound);
    }
}
