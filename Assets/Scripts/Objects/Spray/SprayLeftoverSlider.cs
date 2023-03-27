using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprayLeftoverSlider : MonoBehaviour {
    [SerializeField] private Slider e_Slider = null;
    private float _targetValue = 0.0f;

    private void OnValidate() {
        if(e_Slider == null) {
            Debug.LogWarning($"{gameObject.name}: Slider Object is not validated!");
            gameObject.SetActive(false);
        }
    }

    private void Update() {
        e_Slider.value = Mathf.Lerp(e_Slider.value, _targetValue, 0.1f);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">value must be between 0 ~ 1</param>
    public void ValueChangedTo(float value) {
        if(value < 0 || value > 1.0f)
            return;

        _targetValue = value;
    }
}
