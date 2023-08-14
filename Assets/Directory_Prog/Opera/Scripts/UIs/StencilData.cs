using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Insomnia.Defines;

namespace Insomnia {
    [CreateAssetMenu(fileName = "StencilData", menuName = "ScriptableObject/UI/StencilData", order = 1)]
    public class StencilData : ScriptableObject {
        public List<Stencil> m_stencils = new List<Stencil>();

        public Stencil UnlockStencil(string stencilName) {
            Stencil stencil = null;
            for(int i = 0; i < m_stencils.Count; i++) {
                if(m_stencils[i].Name != stencilName)
                    continue;

                if(m_stencils[i].IsUnlocked == false) {
                    m_stencils[i].IsUnlocked = true;
                    stencil = m_stencils[i];
                }
            }

            return stencil;
        }

#if UNITY_EDITOR
        public Stencil UnlockStencil(int index) {
            Stencil stencil = null;
            if(m_stencils[index].IsUnlocked == false) {
                m_stencils[index].IsUnlocked = true;
                stencil = m_stencils[index];
            }

            return stencil;
        }

        public void InitializeAll() {
            for(int i = 0; i < m_stencils.Count; i++) {
                m_stencils[i].IsUnlocked = false;
            }
        }
#endif
    }

    [Serializable]
    public class Stencil {
        [SerializeField] private StencilType m_type;
        [SerializeField] private string m_stencilName;
        [SerializeField] private Sprite m_maskVisualSprite;
        [SerializeField] private Texture2D m_maskInTexture;
        [SerializeField] private Sprite m_maskOutSprite;
        [SerializeField] private bool m_isUnlocked;

        public Stencil(bool isUnlocked = false)
        {
            m_stencilName = string.Empty;
            m_maskVisualSprite = null;
            m_maskInTexture = null;
            m_maskOutSprite = null;
            m_isUnlocked = false;
        }

        public string Name { get => m_stencilName; }
        public Sprite MaskVisualSprite { get => m_maskVisualSprite; }
        public Texture2D MaskSprite { get => m_maskInTexture; }
        public Sprite MaskOutlineSprite { get => m_maskOutSprite; }
        public bool IsUnlocked { get => m_isUnlocked; set { m_isUnlocked = value; } }
        public Sprite Image { get => m_maskVisualSprite; }
        public StencilType Type { get => m_type; }
    }
}