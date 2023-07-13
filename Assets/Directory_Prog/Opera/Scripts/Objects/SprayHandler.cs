using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class SprayHandler : MonoBehaviour {
        [SerializeField] private GameObject[] m_bodies = null;
        [SerializeField] private GameObject m_arm = null;
        [SerializeField] private Transform _sprayHolder = null;
        [SerializeField] private Spray _spray = null;
        [SerializeField] private float m_defaultSensitivity = 100f;

        [SerializeField] private Animator _handAnim = null;
        private readonly int hashIsFiring = Animator.StringToHash("isFiring");
        private readonly int hashIsShaking = Animator.StringToHash("isShaking");

        private DrawManager _drawManager = null;

        private bool _isEnabled = false;

        private void Start() {
            _spray = DrawManager.Instance.Spray;
            _drawManager = DrawManager.Instance;
        }

        private void OnEnable() {
            _isEnabled = true;
            if(m_arm == null)
                return;

            StartCoroutine(CoStartEnableArm(true, 2.2f));
        }

        private void OnDisable() {
            _isEnabled = false;
            EnableArm(false);
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
            float scrollDelta = Input.mouseScrollDelta.y;
            Debug.Log($"scrollDelta: {scrollDelta}");

            _handAnim.SetBool(hashIsShaking, isShaking);
            

            if(isShaking) {
                Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                _handAnim.speed = Mathf.Clamp01(mouseDelta.magnitude);
                _spray.OnShake(mouseDelta);
                isClicked = false;
            }
            else {
                _handAnim.speed = 1f;
            }
            
            if(DrawManager.Instance.BlackBook.StencilInstaller.IsInstalling == false) {
                _spray.OnClickMouseLeft(isClicked);
                _spray.Radius = scrollDelta;
                _handAnim.SetBool(hashIsFiring, isClicked);
            }
        }

        private void EnableArm(bool isActive) {
            //TODO: trueÀÏ ¶§ ¸ö²ô°í ÆÈÄÑ±â falseÀÏ ¶§ ÆÈ²ô°í ¸öÄÑ±â

            if(m_bodies == null)
                return;

            if(m_bodies.Length <= 0)
                return;

            m_arm.SetActive(isActive);

            for(int i = 0; i < m_bodies.Length; i++) {
                m_bodies[i].SetActive(!isActive);
            }
        }

        private IEnumerator CoStartEnableArm(bool isActive, float times) {
            float curTick = 0f;

            while(true) {
                if(curTick >= times)
                    break;

                curTick += Time.deltaTime;
                yield return null;
            }

            EnableArm(isActive);
            yield break;
        }
    }
}

