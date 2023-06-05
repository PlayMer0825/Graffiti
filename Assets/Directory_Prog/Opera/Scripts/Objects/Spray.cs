using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace OperaHouse {
    public class Spray : MonoBehaviour {
        [SerializeField] ParticleSystem _particle = null;
        [SerializeField] P3dPaintSphere _p3dPaint = null;
        [SerializeField] private const float _sprayCapacity = 100f;
        [SerializeField] private Image _remainFillImage = null;
        [SerializeField] private AudioSource m_audioPlayer = null;
        [SerializeField] private AudioClip m_sprayFireSound = null;

        public float SprayCapacity {get => _sprayCapacity;}

        private float _sprayRemain = 0f;
        private bool _isFiring = false;
        [SerializeField] private bool _canFire = false;
        [SerializeField] private float _sprayDecreaseAmount = 0.4f;
        [SerializeField]private LayerMask _sprayLayer;

        public float SprayRemain { get => _sprayRemain; }
        public Color Color {
            get => _p3dPaint.Color;
            set {
                _p3dPaint.Color = value;
                _particle.startColor = value;
                if(_remainFillImage != null)
                    _remainFillImage.color = value;
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
            m_audioPlayer = GetComponent<AudioSource>();

            _sprayRemain = _sprayCapacity;
        }
        
        public void OnClickMouseLeft(bool isPerformed) {
            if(_canFire == false)
                return;

            if(_isFiring == isPerformed)
                return;

            _isFiring = isPerformed;

            if(_canFire && _isFiring && _sprayRemain > 0) {
                StartCoroutine(CoStartFireSpray());
                m_audioPlayer.Play();
            }
        }

        private IEnumerator CoStartFireSpray() {
            _particle.Play();

            while(true) {
                _sprayRemain -= _sprayDecreaseAmount * Time.deltaTime;
                if((_canFire && _isFiring && _sprayRemain >= 0f) == false)
                    break;

                yield return null;
            }

            _particle.Stop();
            m_audioPlayer.Stop();
            yield break;
        }

        public void OnShake(Vector2 mouseDelta) {
            float delta = mouseDelta.magnitude;
            if(delta  <= 0.2f)
                return;

            _sprayRemain = Mathf.Clamp(_sprayRemain + delta * 0.005f, 0f, _sprayCapacity);
        }

        public void SetSprayRotation() {
            if(DrawManager.Instance.IsDrawing == false)
                return;

            if(DrawManager.Instance.IsAnyPanelOpened())
                return;

            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Camera.main.scaledPixelWidth / 2, Camera.main.scaledPixelHeight / 2));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000f, _sprayLayer)) 
                SetTargetDirection(hit.point);
            else 
                SetTargetDirection(Vector3.zero, false);
        }

        private void SetTargetDirection(Vector3 targetPoint, bool isValid = true) {
            if(isValid == false) {
                _canFire = false;
                return;
            }

            transform.LookAt(targetPoint);
            _canFire = true;
        }

        public void SetColor(Image colorObject) {
            Color = colorObject.color;
        }
    }
}