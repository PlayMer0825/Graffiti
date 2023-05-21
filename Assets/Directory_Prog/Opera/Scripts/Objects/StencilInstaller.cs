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

        public void StartInstallStencil(Texture2D maskTexture, Sprite maskVisual) {
            if(_mask == null)
                return;

            _mask.InitializeMask(maskTexture, maskVisual);
            StartCoroutine(CoStartInstall());
        }

        private IEnumerator CoStartInstall() {
            while(true) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Paintable"))) 
                    _canInstall = true;
                else 
                    _canInstall = false; 

                _mask.SetMaskInstallationAvailable(_canInstall);
                _mask.SetMaskTransform(hit.point, hit.normal);

                if(Input.GetMouseButtonDown(1)) {
                    _mask.ReleaseMask();
                    break;
                }

                if(Input.GetMouseButtonDown(0) && _canInstall) {
                    _mask.InstallMask(hit.point, hit.normal);
                    break;
                }

                float scale = Mathf.Clamp(_mask.transform.localScale.x + Input.GetAxis("Mouse ScrollWheel"), 0.2f, 5f);
                _mask.transform.localScale = new Vector3(scale, scale, scale);
                
                

                yield return null;
            }

            yield break;
        }
    }
}