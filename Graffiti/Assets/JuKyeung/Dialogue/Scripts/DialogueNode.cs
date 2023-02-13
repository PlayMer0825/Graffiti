using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Graffiti.Dialogue
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false;

        [SerializeField]
        string text;

        [SerializeField]
        List<string> children = new List<string>();

        [SerializeField]
        Rect rect = new Rect(0, 0, 200, 100);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rect GetRect()
        {
            return rect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return text;
        }

        /// <summary>
        /// List<string> children 을 리턴
        /// </summary>
        /// <returns>children</returns>
        public List<string> GetChildren()
        {
            return children;
        }

        /// <summary>
        /// bool isPlayerSpeaking 을 리턴
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
#if UNITY_EDITOR

        /// <summary>
        /// 다이얼로그의 위치를 기록합니다. (다이얼로그를 이동시킬 경우)
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");

            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newText"></param>
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newIsPlayerSpeaking"></param>
        public void SetPlayerIsSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Node Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);

        }

        /// <summary>
        /// 자식 다이얼로그에 링크를 추가
        /// </summary>
        /// <param name="childID"></param>
        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");    // 스크립트 오브젝트를 기록하고 다이얼로그를 갱신

            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// 자식 다이얼로그 와의 링크를 삭제
        /// </summary>
        /// <param name="childID"></param>
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

#endif
    }
}