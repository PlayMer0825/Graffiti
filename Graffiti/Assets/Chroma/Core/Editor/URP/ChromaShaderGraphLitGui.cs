#if CHROMA_URP
#if UNITY_2021_2_OR_NEWER

using Chroma;
using UnityEngine;

namespace UnityEditor {
internal class ChromaShaderGraphLitGui : ShaderGraphLitGUI {
    #region Shared Shader Graph GUI code

    private readonly ChromaDrawers _drawers = new ChromaDrawers();
    private MaterialProperty[] _properties;

    public override void OnGUI(MaterialEditor materialEditorIn, MaterialProperty[] properties) {
        _properties = properties;
        materialEditor = materialEditorIn;
#if UNITY_2021_2_OR_NEWER
        base.OnGUI(materialEditorIn, properties);
#else
        var material = materialEditorIn.target as Material;
        DrawSurfaceInputs(material);
#endif
    }

#if UNITY_2021_2_OR_NEWER
    public override void DrawSurfaceInputs(Material material) {
#else
    private void DrawSurfaceInputs(Material material) {
#endif
        ChromaPropertyDrawer.DrawProperties(_properties, materialEditor, _drawers, DefaultPropertyDrawerSg.Draw);
    }

    #endregion
}
}

#endif
#endif