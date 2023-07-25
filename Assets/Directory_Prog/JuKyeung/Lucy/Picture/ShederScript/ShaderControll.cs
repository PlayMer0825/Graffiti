using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShaderControll : MonoBehaviour
{
    public GameObject targetObj;

    private Renderer Objrenderer;

    [Header("적용시킬 셰이더 머테리얼")]
    public Material blurMaterial;
    public Material unblurMaterial;

    [Header("블러의 강도")]
    [SerializeField] private float nowBlurIntensity; // 현재 블러 강도
    public float min_blurAmount = 0.0f;
    public float max_blurAmount = 5.0f;

    public void Awake()
    {
        nowBlurIntensity = max_blurAmount;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        blurMaterial.SetFloat("_Radius", nowBlurIntensity);
    }

}
