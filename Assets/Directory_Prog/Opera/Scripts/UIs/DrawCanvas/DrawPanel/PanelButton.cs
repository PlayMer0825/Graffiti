using OperaHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    [RequireComponent(typeof(Button))]
    public class PanelButton : MonoBehaviour {
        public void OnClick_OpenCanvas(UIPanel panel) {
            panel.OpenPanel();
        }
    }
}