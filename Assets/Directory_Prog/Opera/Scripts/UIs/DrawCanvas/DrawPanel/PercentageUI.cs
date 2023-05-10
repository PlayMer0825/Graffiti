using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class PercentageUI : MonoBehaviour {
        private P3dChangeCounterFill _percentage_Circle = null;
        private P3dChangeCounterText _percentage_Count = null;
        private Image _percentageFill = null;
        private Text _percentageText = null;
        [SerializeField] private Button _clearButton = null;
        [SerializeField] private Button _finishButton = null;
        private P3dChangeCounter _curCounter = null;
        private Button _curExitButton = null;

        public bool IsFinished { get => _percentageFill.fillAmount >= 0.3f; }

        private void Awake() {
            _percentage_Circle = GetComponentInChildren<P3dChangeCounterFill>();
            _percentage_Count = GetComponentInChildren<P3dChangeCounterText>();
            _percentageFill = GetComponentInChildren<Image>();
            _percentageText = GetComponentInChildren<Text>();

            _curExitButton = _finishButton;
        }

        private void OnEnable() {
            RegisterForChangeCounter(null);
        }

        private void Update() {
            if(DrawManager.Instance.IsDrawing == false)
                return;

            bool maskEnabled = DrawManager.Instance.Stencil.MaskEnabled;

            if(( maskEnabled && IsStencilAllFilled ()) ||  IsFinished)
                _curExitButton.gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space)) {
                if(_curExitButton.gameObject.activeSelf == false)
                    return;

                if(maskEnabled)
                    DrawManager.Instance.Stencil.ReleaseMask();
                else 
                    DrawManager.Instance.FinishDrawing();
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

        public void SetDrawFinishMode(bool isStencilActive) {
            if(isStencilActive) {
                _curExitButton = _clearButton;
                _finishButton.gameObject.SetActive(false);
            }
            else {
                _curExitButton = _finishButton;
                _clearButton.gameObject.SetActive(false);
            }
        }

        private bool IsStencilAllFilled() {
            StencilMask mask = DrawManager.Instance.Stencil;
            if(mask == null)
                return false;

            //_curCounter.Count
            if(mask.MaskEnabled == false)
                return false;

            if(mask.MaskPixelCount <= _curCounter.Count)
                return true;
            return false;
        }
    }

}