using UnityEngine;

namespace Insomnia {
    public class InteractManager : MonoBehaviour {
        private static InteractManager _instance =null;
        public static InteractManager Instance {
            get => _instance;
        }

        private Interactable _interactable = null;
        private bool _isInteracting = false;

        public bool IsInteracting { get => _isInteracting; }

        private void Awake() {
            if(_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        public void StartInteractWith(in Interactable interactable) {
            _interactable = interactable;
            _isInteracting = true;
        }

        public void FinishInteract() {
            if(_interactable == null)
                return;

            _interactable.FinishInteract();
            _interactable = null;
            _isInteracting = false;
        }
    }
}
