using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera TPS;

    public AudioSource click;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)&&TPS.enabled==false)
        {
            click.Play();
        }
    }
}
