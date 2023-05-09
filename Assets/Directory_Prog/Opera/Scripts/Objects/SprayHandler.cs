using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class SprayHandler : MonoBehaviour {
        [SerializeField] private Transform _sprayHolder = null;
        [SerializeField] private Spray _spray = null;

        [SerializeField] private Animator _handAnim = null;
        private readonly int hashIsFiring = Animator.StringToHash("isFiring");

        private DrawManager _drawManager = null;

        private bool _isEnabled = false;

        private void Start() {
            _spray = DrawManager.Instance.Spray;
            _drawManager = DrawManager.Instance;
        }

        private void OnEnable() {
            _isEnabled = true;
        }

        private void OnDisable() {
            _isEnabled = false;
        }

        private void Update() {
            if(_isEnabled == false)
                return;

            if(_spray == null)
                return;

            if(_drawManager.IsAnyPanelOpened())
                return;

            _spray.transform.position = _sprayHolder.transform.position;
            _spray.SetSprayRotation();

            bool isShaking = Input.GetMouseButton(2);
            bool isClicked = Input.GetMouseButton(0);

            if(isShaking) {
                _spray.OnShake(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
                isClicked = false;
            }

            _spray.OnClickMouseLeft(isClicked);
            _handAnim.SetBool(hashIsFiring, isClicked);
        }
    }
}

