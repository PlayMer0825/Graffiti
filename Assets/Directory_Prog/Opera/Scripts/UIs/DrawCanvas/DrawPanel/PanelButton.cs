using UnityEngine;
using UnityEngine.UI;

namespace Insomnia {
    [RequireComponent(typeof(Button))]
    public class PanelButton : MonoBehaviour {
        public void OnClick_OpenCanvas(UIPanel panel) {
            panel.OpenPanel();
        }
    }
}