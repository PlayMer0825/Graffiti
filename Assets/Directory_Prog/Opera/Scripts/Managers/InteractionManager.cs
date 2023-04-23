using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
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

        public void FinishedInteract(Interactable interacted) {
            if(interacted == null)
                return;

            _isInteracting = false;
            _curInteracting = null;
        }
    }
}
