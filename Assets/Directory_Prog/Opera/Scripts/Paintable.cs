using Cinemachine;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class Paintable : QuestHandler {
        #region GameObjects & Components
        [SerializeField] private P3dPaintable _paintable = null;

        #endregion

        public virtual void Awake() {
            _paintable = GetComponentInChildren<P3dPaintable>();
        }

        protected void SetP3dPaintable(bool value) { _paintable.enabled = value; }

    }
}