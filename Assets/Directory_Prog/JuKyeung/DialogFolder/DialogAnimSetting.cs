using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimSetting : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = playerAnimator.gameObject.GetComponent <Animator>();
    }

    public void EndDialog_Anim()
    {
        playerAnimator.SetBool("is_titi_idle_bool", false);
    }
}
