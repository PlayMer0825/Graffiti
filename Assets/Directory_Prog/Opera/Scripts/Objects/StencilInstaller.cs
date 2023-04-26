using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class StencilInstaller : MonoBehaviour {
        [SerializeField] private StencilMask _mask = null;

        private bool _canInstall = false;
        private bool _isInstalled = false;

        public bool IsInstalled { get => _isInstalled; }

        public void StartInstallStencil(Texture maskTexture, Sprite maskVisual) {
            if(_mask == null)
                return;

            _mask.InitializeMask(maskTexture, maskVisual);
            StartCoroutine(CoStartInstall());
        }

        private IEnumerator CoStartInstall() {
            while(true) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Paintable"))) {
                    Debug.Log($"hit.name: {hit.collider.name} hit.tag: {hit.collider.tag}");
                    _mask.SetMaskVisible(true);
                    _mask.SetMaskTransform(hit.point, hit.normal);

                    if(hit.collider.CompareTag("Paintable") == true) {
                        //TODO: 설치 범위 체크하는 함수 추가 필요. Collider.bounds써야할듯
                        _canInstall = true;
                    }
                    else {
                        //TODO: 설치 범위 체크하는 함수 추가 필요. Collider.bounds써야할듯
                        _canInstall = false;
                    }
                }
                else {
                    _canInstall = false; 
                    _mask.SetMaskVisible(false);
                }
                _mask.SetMaskInstallationAvailable(_canInstall);

                if(Input.GetMouseButtonDown(1)) {
                    _mask.ReleaseMask();
                    break;
                }

                if(Input.GetMouseButtonDown(0) && _canInstall) {
                    _mask.InstallMask(hit.point, hit.normal);
                    break;
                }

                yield return null;
            }

            yield break;
        }
    }
}