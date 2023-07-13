using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Insomnia {
    public class SprayRemainReader : MonoBehaviour {
        [SerializeField] private Slider _sprayRemainSlider = null;
        [SerializeField] private Spray _spray = null;

        private void Awake() {
            _sprayRemainSlider = GetComponent<Slider>();
        }

        private void Update() {
            _sprayRemainSlider.value = _spray.SprayRemain / _spray.SprayCapacity;
        }
    }
}

