using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class SprayHandler : MonoBehaviour {
        [SerializeField] private Transform _sprayHolder = null;
        [SerializeField] private Spray _spray = null;

        [SerializeField] private Animator _handAnim = null;
        private readonly int hashIsFiring = Animator.StringToHash("isFiring");

        private bool _isEnabled = false;

        private void Start() {
            _spray = DrawManager.Instance.Spray;

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

            _spray.transform.position = _sprayHolder.transform.position;
            _spray.transform.rotation = _sprayHolder.transform.rotation;

            bool isClicked = Input.GetMouseButton(0);

            _spray.OnClickMouseLeft(isClicked);
            _handAnim.SetBool(hashIsFiring, isClicked);
        }
    }
}

