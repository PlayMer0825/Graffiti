#if CHROMA_SG || CHROMA_URP || CHROMA_HDRP
using System.Reflection;
using UnityEditor;
using UnityEditor.ShaderGraph;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

namespace Chroma {
public static class DefaultPropertyDrawerSg {
    public static void Draw(MaterialEditor editor, Material material, MaterialProperty property, string label,
                            string tooltip) {
#if UNITY_2021_2_OR_NEWER
        // Use SG default drawing only if no tooltip. SG doesn't handle tooltips, but handles Vector 2/3.
        if (material.shader.IsShaderGraphAsset() && string.IsNullOrEmpty(tooltip)) {
            var field = property.GetType().GetField("m_DisplayName", BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(property, label);
            ShaderGraphPropertyDrawers.DrawShaderGraphGUI(editor, new[] { property });
            if (property.type == MaterialProperty.PropType.Vector) {
                EditorGUILayout.Space(-18);
            }
        } else {
#endif
            // Drawing SG Vector2/3 in this case is broken.
            var guiContent = new GUIContent(label, tooltip);
            editor.ShaderProperty(property, guiContent);
#if UNITY_2021_2_OR_NEWER
        }
#endif
    }
}
}
#endif