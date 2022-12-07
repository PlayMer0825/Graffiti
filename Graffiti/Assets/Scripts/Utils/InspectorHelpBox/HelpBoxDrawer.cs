using UnityEngine;
using UnityEditor;
using System;

namespace Giacomelli.Framework {
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public class HelpBoxDrawer : PropertyDrawer {
        const float XPadding = 30f;
        const float YPadding = 5f;
        const float DefaultHeight = 20f;
        const float DocsButtonHeight = 20f;
        float _height;

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label) {

            var attr = attribute as HelpBoxAttribute;
            if(property.objectReferenceValue == null)
                CalculateHeight(attr);

            EditorGUI.PropertyField(position, property, label, true);
            if(property.objectReferenceValue != null)
                return;

            position = new Rect(
                XPadding,
                position.y + EditorGUI.GetPropertyHeight(property, label, true) + YPadding,
                position.width - XPadding,
                _height);


            EditorGUI.HelpBox(position, attr.Text, (MessageType)attr.Type);

            if(!string.IsNullOrEmpty(attr.DocsUrl)) {
                position = new Rect(
                    position.x + position.width - 40,
                    position.y + position.height - DocsButtonHeight,
                    40,
                    DocsButtonHeight);


                if(GUI.Button(position, "Docs")) {
                    if(attr.DocsUrl.StartsWith("http"))
                        Application.OpenURL(attr.DocsUrl);
                    else
                        Application.OpenURL($"https://docs.unity3d.com/ScriptReference/{attr.DocsUrl}");
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, true) + _height + 10;
        }

        void CalculateHeight(HelpBoxAttribute attr) {
            _height = ( attr.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Length + 1 ) * DefaultHeight;

            if(!string.IsNullOrEmpty(attr.DocsUrl))
                _height += DocsButtonHeight;
        }
    }
}