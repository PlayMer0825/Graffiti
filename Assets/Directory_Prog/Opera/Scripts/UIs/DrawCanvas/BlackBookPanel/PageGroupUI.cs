using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class PageGroupUI : MonoBehaviour {
        List<PageElement> _elements = new List<PageElement>();

        private void Awake() {
            if(_elements.Count > 0)
                return;

            PageElement[] elements = GetComponentsInChildren<PageElement>();
            if(elements.Length != 6)
                return;

            for(int i = 0; i < elements.Length; i++) {
                _elements.Add(elements[i]);
            }
        }

        /// <summary>
        /// 해당 페이지에 존재하는 버튼들의 텍스쳐를 변경하는 함수.
        /// </summary>
        public void InitializePage(List<Stencil> stencils) {
            Stencil nullStencil = new Stencil(){ IsUnlocked = false };

            if(stencils == null || stencils.Count == 0) {
                for(int i = 0; i < _elements.Count; i++) {
                    _elements[i].InitElement(nullStencil);
                }

                return;
            }

            for(int i = 0; i < stencils.Count; i++) {
                _elements[i].InitElement(stencils[i]);
            }

            for(int i = stencils.Count; i < _elements.Count; i++) {
                _elements[i].InitElement(nullStencil);
            }
        }
    }
}