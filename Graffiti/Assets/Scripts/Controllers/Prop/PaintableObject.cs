using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: RequireComponent로 P3D 컴포넌트 전부 불러오기
[RequireComponent(typeof(P3dPaintable))]
[RequireComponent(typeof(P3dMaterialCloner))]
[RequireComponent(typeof(P3dPaintableTexture))]
[RequireComponent(typeof(BoxCollider))]
public class PaintableObject : Interactable {
    #region Components
    [SerializeField] private P3dPaintable _p3dPaintable = null;
    [SerializeField] private P3dMaterialCloner _p3dMatCloner = null;
    [SerializeField] private P3dPaintableTexture _p3dTex = null;
    [SerializeField] private BoxCollider _interactTrigger = null;


    #endregion

    #region GameObjects


    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(_p3dPaintable == null)
            _p3dPaintable = GetComponent<P3dPaintable>();
        if(_p3dMatCloner == null)
            _p3dMatCloner = GetComponent<P3dMaterialCloner>();
        if(_p3dTex == null)
            _p3dTex = GetComponent<P3dPaintableTexture>();

        if(_interactTrigger == null)
            _interactTrigger = GetComponent<BoxCollider>();
    }

    #endregion

    #region Unity Event Functions


    #endregion

    #region User Defined Functions


    #endregion

    #region Override Functions
    public override void OnInteract() {
        
    }

    #endregion
}
