using UnityEngine;

namespace Dustyroom {
[RequireComponent(typeof(MeshRenderer)), ExecuteAlways]
public class MultiCurveSetter : MonoBehaviour {
    public string propertyName = "_Displacement";
    public Material[] materials;

    private void OnValidate() {
        var meshRenderer = GetComponent<MeshRenderer>();
        var material = meshRenderer.sharedMaterial;
        var texture = material.GetTexture(propertyName) as Texture2D;

        foreach (var m in materials) {
            m.SetTexture(propertyName, texture);
        }
    }
}
}