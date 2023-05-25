using OperaHouse;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageElement : MonoBehaviour {
    private Button _elemButton = null;
    private Image _elemImage = null;

    private Stencil _stencil;

    private Texture _maskTexture = null;
    private Sprite _maskVisual = null;

    private bool _isInitialized = false;

    private void Awake() {
        _elemButton = GetComponent<Button>();
        _elemImage = GetComponent<Image>();
    }

    public void InitElement(Stencil data) {
        if(data.MaskSprite == null) {
            _elemImage.sprite = null;
            _stencil = data;
            _isInitialized = false;
            _elemButton.interactable = false;
        }
        else if (data.IsUnlocked == false) {
            _elemImage.sprite = data.MaskOutlineSprite;
            _stencil = data;
            _isInitialized = true;
            _elemButton.interactable = false;
        }
        else {
            _elemImage.sprite = data.MaskOutlineSprite;
            _stencil = data;
            _isInitialized = true;
            _elemButton.interactable = true;
        }
    }

    public void OnClick_SelectMask() {
        if(_isInitialized == false && _stencil.IsUnlocked == false)
            return;

        DrawManager.Instance.BlackBook.InstallStencil(_stencil);
    }
}
