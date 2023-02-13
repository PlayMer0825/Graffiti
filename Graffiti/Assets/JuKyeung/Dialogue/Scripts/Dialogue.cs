using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Graffiti.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        /// <summary>
        /// 노드 목록 
        /// </summary>
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();

        /// <summary>
        /// 새롭게 생성되는 노드의 초기 위치는 해당 위치입니다. 
        /// </summary>
        [NonSerialized]
        Vector2 newNodeOffset = new Vector2(250, 0);

        /// <summary>
        /// 생성된 노드들의 uniqueID(이름) 과 DialogueNode 의 정보를 담고 있습니다. 
        /// </summary>
        [NonSerialized]
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        /// <summary>
        /// RootNode를 반환합니다
        /// </summary>
        /// <returns></returns>
        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }

#if UNITY_EDITOR

        /// <summary>
        /// 노드를 생성하는 메서드입니다.
        /// </summary>
        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetPlayerIsSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
            }
            Undo.RegisterCreatedObjectUndo(newNode, "Create Dialogue Node"); // 오브젝트 생성 이후 호출
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                Undo.RecordObject(this, "Added Dialogue Node");
            }
            nodes.Add(newNode);
            OnValidate();
        }

        /// <summary>
        /// 노드를 삭제하는 메서드입니다.
        /// </summary>
        /// <param name="nodeToDelete"></param>
        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");

            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);

        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif



        /// <summary>
        /// 부모 다이얼로그가 삭제될 경우 연결된 자식 노드를 함께 삭제 시키는 메서드입니다.
        /// </summary>
        /// <param name="nodeToDelete"></param>



        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                CreateNode(null);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this); // 프로젝트에 포함된 에셋에 접근하게 해준다
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }
}