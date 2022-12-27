using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableObject : Interactable
{
    #region Components
    [Header("PaintableObject Instances")]
    [Tooltip("Model Must be Contained P3dPaintable Component")]
    [SerializeField] private P3dPaintable _paintable = null;

    #endregion

    public override void OnInteract() {
        base.OnInteract();
        _paintable.enabled = true;
    }

    public override void OffInteract() {
        base.OffInteract();
        _paintable.enabled = false;
    }

    protected override void OnPlayerTriggered() {

    }

    protected override void OnPlayerUntriggered() {

    }
}
