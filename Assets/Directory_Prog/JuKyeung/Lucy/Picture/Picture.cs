using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    private RectTransform camerapanel_RectPos;

    public bool isCameraMode = false;
    public float moveSpeed = 10f;

    public Transform targetObj;

    private Renderer Objrenderer;

    Vector2 panelPosition;
    [SerializeField] float distance;

    public bool isClearCheck;

    [Header("적용시킬 셰이더 머테리얼")]
    public Material blurMaterial;

    [Header("블러의 강도")]
    [SerializeField] private float nowBlurIntensity; // 현재 블러 강도
    public float min_blurAmount = 0.0f;
    public float max_blurAmount = 5.0f;

    private void Awake()
    {
        nowBlurIntensity = max_blurAmount;
        isClearCheck = false;
    }
    private void Start()
    {
        camerapanel_RectPos = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        panelPosition = camerapanel_RectPos.position;
        panelPosition = Vector2.Lerp(panelPosition, mousePosition, moveSpeed * Time.deltaTime);
        camerapanel_RectPos.position = panelPosition;

        Position();
        blurMaterial.SetFloat("_Radius", nowBlurIntensity);
        PicturePuzzleClearCheck(nowBlurIntensity);

        if (isClearCheck == true)
        {
            nowBlurIntensity = 0;
        }
    }

    void Position()
    {
        distance = Vector3.Distance(panelPosition, targetObj.position);

        nowBlurIntensity = Mathf.Lerp(min_blurAmount, max_blurAmount, distance / 100f);
    }

    void PicturePuzzleClearCheck(float checkGage)
    {
        if (checkGage < 0.7f)
        {
            isClearCheck = true;
        }
    }

    void ClearCheck()
    {

    }
}
