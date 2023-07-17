using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insomnia {
    public class StencilInstaller : MonoBehaviour {
        [SerializeField] private StencilMask _mask = null;

        private bool _canInstall = false;
        private bool _isInstalled = false;
        private bool _isInstalling = false;

        public bool IsInstalled { get => _isInstalled; }
        public bool IsInstalling { get => _isInstalling; }

        public void StartInstallStencil(Texture2D maskTexture, Sprite maskVisual) {
            if(_mask == null)
                return;

            _mask.InitializeMask(maskTexture, maskVisual);
            StartCoroutine(CoStartInstall());
        }

        private IEnumerator CoStartInstall() {
            _isInstalling = true;
            while(true) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));

                if(Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Paintable"))) 
                    _canInstall = true;
                else 
                    _canInstall = false; 

                _mask.SetMaskInstallationAvailable(_canInstall);
                _mask.SetMaskTransform(hit.point + new Vector3(0, 0, -0.03f), hit.normal);

                if(_canInstall == false) {
                    yield return null;
                    continue;
                }

                if(Input.GetMouseButtonDown(1)) {
                    _mask.ReleaseMask();
                    break;
                }

                if(Input.GetMouseButtonDown(0) && _canInstall) {
                    _mask.InstallMask(hit.point, hit.normal);
                    break;
                }

                float scale = Mathf.Clamp(_mask.transform.localScale.x + Input.GetAxis("Mouse ScrollWheel"), 1f, 6f);
                _mask.transform.localScale = new Vector3(scale, scale, scale);
                
                yield return null;
            }
            _isInstalling = false;
            yield break;
        }
    }
}