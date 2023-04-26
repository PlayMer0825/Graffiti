using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class Spray : MonoBehaviour {
        [SerializeField] ParticleSystem _particle = null;
        [SerializeField] P3dPaintSphere _p3dPaint = null;
        [SerializeField] private const float _sprayCapacity = 100f;
        public float SprayCapacity {get => _sprayCapacity;}

        private float _sprayRemain = 0f;
        private bool _isFiring = false;
        private WaitForSeconds _sprayDecreaseInterval = null;
        [SerializeField] private float _sprayDecreaseIntervalTime = 1f;
        [SerializeField] private float _sprayDecreaseIntervalAmount = 0.4f;

        public float SprayRemain { get => _sprayRemain; }
        public Color Color {
            get => _p3dPaint.Color;
            set {
                _p3dPaint.Color = value;
                _particle.startColor = value;
            }
        }

        public float Opacity {
            get => _p3dPaint.Opacity;
            set {
                _p3dPaint.Opacity = value;
            }
        }

        public float Radius {
            get => _p3dPaint.Radius;
            set {
                _p3dPaint.Radius = value;
            }
        }

        private void Awake() {
            _particle = GetComponent<ParticleSystem>();
            _p3dPaint = GetComponent<P3dPaintSphere>();

            _sprayRemain = _sprayCapacity;
            _sprayDecreaseInterval = new WaitForSeconds(_sprayDecreaseIntervalTime);
        }

        public void OnClickMouseLeft(bool isPerformed) {
            if(_isFiring == isPerformed)
                return;

            _isFiring = isPerformed;
            if(_isFiring)
                StartCoroutine(CoStartFireSpray());
        }

        private IEnumerator CoStartFireSpray() {
            _particle.Play();

            while(_isFiring && _sprayRemain >= 0f) {
                _sprayRemain -= _sprayDecreaseIntervalAmount;
                yield return _sprayDecreaseInterval;
            }

            _particle.Stop();
            yield break;
        }
    }
}