using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Graffiti.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;

        [NonSerialized]
        GUIStyle nodeStyle;

        [NonSerialized]
        GUIStyle playerNodeStyle;

        [NonSerialized]
        DialogueNode draggingNode = null;

        [NonSerialized]
        Vector2 draggingOffset;

        [NonSerialized]
        DialogueNode creatingNode = null;

        [NonSerialized]
        DialogueNode deletingNode = null;

        /// <summary>
        /// 링크된 부모 노드
        /// </summary>
        [NonSerialized]
        DialogueNode linkingParentNode = null;

        Vector2 scrollPosition;

        [NonSerialized]
        bool draggingCanvas = false;

        [NonSerialized]
        Vector2 draggingCanvasOffset;

        /// <summary>
        /// 다이얼로그 에디터 캔버스 사이즈
        /// </summary>
        const float canvasSize = 4000;
        const float backgroundSize = 2048;


        /// <summary>
        /// 
        /// </summary>
        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            //Debug.Log("ShowEditorWindow");
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }


        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;

            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;

            playerNodeStyle.normal.textColor = Color.white;
            playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }

        }

        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected ");
            }
            else
            {
                ProcessEvents();

                // 에디터에 스크롤뷰를 추가 -> 크기에 맞게 자동으로 스크롤이 추가
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                //Debug.Log(scrollPosition);

                //GUILayout.Label("이것은 다이얼로그의 데이터 덩어리들이고 ");

                // 다이얼로그 에디터의 크기를 4000 x 2048 으로 규정
                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTexture = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, texCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }

                if (deletingNode != null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }

            }

        }


        /// <summary>
        /// 노드들을 드래그 & 클릭으로 움직일 수 있게 하는 메서드입니다 
        /// </summary>
        private void ProcessEvents()
        {

            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);

                // 노드를 움직이고 있을 경우 
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;

                    Selection.activeObject = draggingNode;
                }
                // 노드를 움직이다가 멈췄을 경우
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }
        }

        /// <summary>
        /// 다이얼로그 노드를 그리는 함수입니다. 
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.IsPlayerSpeaking())
            {
                style = playerNodeStyle;
            }
            GUILayout.BeginArea(node.GetRect(), style); // 시작 영역 
            EditorGUI.BeginChangeCheck();

            //노드의 라벨 필드
            EditorGUILayout.LabelField("<Node> ", EditorStyles.whiteLabel);

            //노드의 TextField 는 각각 Text입력 필드입니다. 
            node.SetText(EditorGUILayout.TextField(node.GetText()));

            GUILayout.BeginHorizontal(); // Begin - End Horizontal 영역 까지를 수평으로 배치 

            // 다이얼로그를 추가하는 버튼 
            if (GUILayout.Button("추가"))
            {
                creatingNode = node;
            }

            // 다이얼로그를 삭제하는 버튼 
            if (GUILayout.Button("삭제"))
            {
                deletingNode = node;
            }

            GUILayout.EndHorizontal();

            DrawLinkButtons(node);

            GUILayout.EndArea(); // 끝 영역 
        }

        /// <summary>
        /// 이미 생성된 다이얼로그 간의 연결을 만들어주는 메서드
        /// </summary>
        private void DrawLinkButtons(DialogueNode node)
        {
            // 다이얼로그끼리 연결 
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("다이얼로그 링크"))
                {
                    linkingParentNode = node;
                }
            }

            // 연결하는 것을 취소 
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("취소"))
                {
                    linkingParentNode = null;
                }
            }

            // 자식 노드와의 링크를 해제
            else if (linkingParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("링크해제"))
                {
                    linkingParentNode.RemoveChild(node.name);   // 링크되는 부모 노드의 자식 노드를 가져와 목록에 현재 노드를 제거
                    linkingParentNode = null;
                }
            }

            // 자식 노드 지정해서 링크 
            else
            {
                if (GUILayout.Button("자식 다이얼로그로 지정"))
                {
                    Undo.RecordObject(selectedDialogue, "Add Dialogue Link");
                    linkingParentNode.AddChild(node.name);  // 링크되는 부모 노드의 자식 노드를 가져와 목록에 현재 노드를 추가
                    linkingParentNode = null;
                }
            }
        }

        /// <summary>
        /// 노드를 잇는 선에 관련된 메서드  / 베지어 곡선 사용
        /// </summary>
        /// <param name="node"></param>
        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset,
                    Color.white, null, 4f);
            }
        }

        /// <summary>
        /// 각 노드를 잇는 점들의 위치를 구하는 메서드
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}