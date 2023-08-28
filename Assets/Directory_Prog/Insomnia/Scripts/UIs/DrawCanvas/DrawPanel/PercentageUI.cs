using PaintIn3D;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Insomnia {
    public class PercentageUI : MonoBehaviour {
        private P3dChangeCounterFill _percentage_Circle = null;
        private P3dChangeCounterText _percentage_Count = null;
        [SerializeField] private Image _percentageFill = null;
        [SerializeField] private TMP_Text _percentageText = null;
        [SerializeField] private Button _clearButton = null;
        [SerializeField] private Button _finishButton = null;
        [SerializeField] private Color m_defaultColor = Color.white;
        [SerializeField] private Color m_finishColor = Color.yellow;

        private P3dChangeCounter _curCounter = null;
        private Button _curExitButton = null;
        private Func<bool> _checkFunc = null;

        [SerializeField] private float m_finishAmount = 0.25f;

        public UnityEvent onClearCondSolved = new UnityEvent();
        public UnityEvent onFinishCondSolved = new UnityEvent();

        public bool IsFinished { get => _percentageFill.fillAmount >= 0.3f; }

        private void Awake() {
            _percentage_Circle = GetComponentInChildren<P3dChangeCounterFill>();
            _percentage_Count = GetComponentInChildren<P3dChangeCounterText>();

            

            _curExitButton = _finishButton;
            _checkFunc = CheckDrawFinish;
        }

        private void OnEnable() {
            RegisterForChangeCounter(null);
        }

        private void Update() {
            if(DrawManager.Instance.IsDrawing == false)
                return;

            if(_curCounter != null) {
                Color color = Color.Lerp(m_defaultColor, m_finishColor, _curCounter.Ratio);
                _percentageText.color = _percentageFill.color = color;
            }


            if(_checkFunc.Invoke()) {
                if(_curExitButton.gameObject.activeSelf == false) {
                    _curExitButton.gameObject.SetActive(true);
                    DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Stencil_Finished);
                    if(_curExitButton == _clearButton)
                        onClearCondSolved?.Invoke();
                    else if(_curExitButton == _finishButton)
                        onFinishCondSolved?.Invoke();
                }
            }

            if(Input.GetKeyDown(KeyCode.Space)) {
                if(_curExitButton.gameObject.activeSelf == false)
                    return;

                if(_clearButton.gameObject.activeSelf) {
                    DrawManager.Instance.Stencil.ReleaseMask();
                }
                else if(_finishButton.gameObject.activeSelf) {
                    DrawManager.Instance.FinishDrawing();
                }
            }
        }

        private void OnDisable() {
            ReleaseCounter();
        }

        public void RegisterForChangeCounter(P3dChangeCounter counter) {
            if(counter == null)
                return;

            _curCounter = counter;
            _percentage_Circle.Counters.Add(_curCounter);
            _percentage_Count.Counters.Add(_curCounter);

        }

        public void ReleaseCounter() {
            if(_curCounter == null)
                return;

            if(_percentage_Circle.Counters.Contains(_curCounter))
                _percentage_Circle.Counters.Remove(_curCounter);
            if(_percentage_Count.Counters.Contains(_curCounter))
                _percentage_Count.Counters.Remove(_curCounter);

            _curCounter = null;
        }

        public void SetExitMethod(bool isStencilActive) {
            if(isStencilActive) {
                _curExitButton = _clearButton;
                _clearButton.gameObject.SetActive(false);
                _finishButton.gameObject.SetActive(false);
                _checkFunc = CheckStencilFinish;
            }
            else {
                _curExitButton = _finishButton;
                _clearButton.gameObject.SetActive(false);
                _finishButton.gameObject.SetActive(false);
                _checkFunc = CheckDrawFinish;
            }
        }

        private bool CheckStencilFinish() {
            StencilMask mask = DrawManager.Instance.Stencil;
            if(mask == null) return false;

            DrawObject obj = DrawManager.Instance.curDrawing;
            if(obj == null) return false;

            return _percentageFill.fillAmount * obj.MaxPixelCount >= mask.MaskPixelCount;
        }

        private bool CheckDrawFinish() {
            return _percentageFill.fillAmount >= m_finishAmount;
        }
    }
}