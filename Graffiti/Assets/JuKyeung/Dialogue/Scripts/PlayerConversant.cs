using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graffiti.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        /// <summary>
        /// Dialogue
        /// </summary>
        [SerializeField] Dialogue currentDialogue;

        /// <summary>
        /// DialogueNode
        /// </summary>
        DialogueNode currentNode = null;
        /// <summary>
        /// 시작할 때 
        /// </summary>
        /// 


        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();
        }
        public string GetText()
        {
            if (currentDialogue == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public void Next()
        {
            DialogueNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Count());
            currentNode = children[randomIndex];
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }


}

