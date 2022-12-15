using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(P3dPaintable))]
[RequireComponent(typeof(P3dMaterialCloner))]
[RequireComponent(typeof(P3dPaintableTexture))]

public class PaintableObject : Interactable
{
    #region Components
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
        base.OnPlayerTriggered();
    }

    protected override void OnPlayerUntriggered() {
        base.OnPlayerUntriggered();
    }
}
