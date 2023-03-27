using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    /// <summary>
    /// 2D Texture 오브젝트에만 적용
    /// </summary>
    public class Paintable_Stencil : Paintable, Mountable, Interactable{


        #region Mountable Interface
        public void StartMount() {
            //TODO: 반투명하게 만들고, 코루틴써서 Paintable_Wall에 레이캐스팅.

            StartCoroutine(CoStartMount());
        }

        public void MountOn() {
            //TODO: 여긴 아마 현재 충돌하고 있는 Paintable_Wall이랑 충돌한 위치좌표 주면 될듯.
            SetP3dPaintable(true);

            StopCoroutine(CoStartMount());
        }

        public void FinishMount() {
            //TODO: 다시 투명하게 만들고 코루틴 종료

            StopCoroutine(CoStartMount());
        }

        private IEnumerator CoStartMount() {

            yield break;
        }

        #endregion

        #region Interactable Interface


        public void StartInteract() {
            
        }

        public void FinishInteract() {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}