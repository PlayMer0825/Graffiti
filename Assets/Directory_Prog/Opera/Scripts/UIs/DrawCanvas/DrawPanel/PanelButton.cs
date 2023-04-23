using OperaHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PanelButton : MonoBehaviour {
    public CanvasType type;

    public void OnClick_OpenCanvas() {
        DrawManager.Instance.OpenCanvas(type);
    }
}
