namespace Insomnia {
    public class InteractionManager : Singleton<InteractionManager> {
        private Interactable _curInteracting = null;
        private bool _isInteracting = false;

        public bool IsInteracting { get => _isInteracting; }
        public Interactable CurInteracting { get => _curInteracting; }

        protected override void Awake() {
            base.Awake();
        }

        public void StartedInteract(Interactable interacted) {
            if(interacted == null)
                return;

            _isInteracting = true;
            _curInteracting = interacted;
        }

        public void FinishedInteract() {
            if(_curInteracting == null)
                return;

            _curInteracting.FinishInteract();
            _isInteracting = false;
            _curInteracting = null;
        }

        public void SetCurInteractActivation(bool isSet) {
            if(_curInteracting == null)
                return;

            _curInteracting.IsInteractable = isSet;
        }
    }
}
