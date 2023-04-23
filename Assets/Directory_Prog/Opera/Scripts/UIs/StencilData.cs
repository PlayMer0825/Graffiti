using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    [CreateAssetMenu(fileName = "StencilData", menuName = "ScriptableObject/UI/StencilData", order = 1)]
    public class StencilData : ScriptableObject {
        public List<Stencil> _stencils = new List<Stencil>();
    }

    [Serializable]
    public struct Stencil {
        [SerializeField] private Texture _maskSprite;
        [SerializeField] private Sprite _maskOutlineSprite;
        [SerializeField] private bool _isUnlocked;

        public Stencil(bool isUnlocked = false)
        {
            _maskSprite = null;
            _maskOutlineSprite = null;
            _isUnlocked = false;
        }

        public Texture MaskSprite { get => _maskSprite; }
        public Sprite MaskOutlineSprite { get => _maskOutlineSprite; }
        public bool IsUnlocked { get => _isUnlocked; set => _isUnlocked = value; }
    }
}