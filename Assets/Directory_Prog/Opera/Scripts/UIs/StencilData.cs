using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insomnia {
    [CreateAssetMenu(fileName = "StencilData", menuName = "ScriptableObject/UI/StencilData", order = 1)]
    public class StencilData : ScriptableObject {
        public List<Stencil> _stencils = new List<Stencil>();
    }

    [Serializable]
    public struct Stencil {
        [SerializeField] private Sprite _maskVisualSprite;
        [SerializeField] private Texture2D _maskInTexture;
        [SerializeField] private Sprite _maskOutSprite;
        [SerializeField] private bool _isUnlocked;

        public Stencil(bool isUnlocked = false)
        {
            _maskVisualSprite = null;
            _maskInTexture = null;
            _maskOutSprite = null;
            _isUnlocked = false;
        }

        public Sprite MaskVisualSprite { get => _maskVisualSprite; }
        public Texture2D MaskSprite { get => _maskInTexture; }
        public Sprite MaskOutlineSprite { get => _maskOutSprite; }
        public bool IsUnlocked { get => _isUnlocked; set => _isUnlocked = value; }
    }
}