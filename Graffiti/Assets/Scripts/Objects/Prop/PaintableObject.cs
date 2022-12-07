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
    private P3dPaintable _paintable = null;

    #endregion

    protected override void OnInteract() {
        base.OnInteract();
        _paintable.enabled = true;

    }

    protected override void OffInteract() {
        base.OffInteract();
        _paintable.enabled = false;
    }
}
