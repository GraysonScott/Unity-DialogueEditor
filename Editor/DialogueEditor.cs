using System;
using System.Collections.Generic;
using Editor.Nodes;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    public class DialogueEditor : EditorWindow
    {
        #region Variables
        static List<BaseNode> windows = new List<BaseNode>();
        private Vector3 _mousePosition;
        private bool _makeTransition;
        private bool _clickedOnWindow;
        private BaseNode _selectedNode;

        private enum UserActions
        {
            AddDialogue,AddTransitionNode,DeleteNode,CommentNode,AddActionNode 
        }
        #endregion

        #region Init

        [MenuItem("Window/DialogueEditor")]
        private static void ShowEditor()
        {
            DialogueEditor editor = GetWindow<DialogueEditor>();
            editor.titleContent = new GUIContent("Node Based Dialogue Editor");
            editor.minSize = new Vector2(800,600);
        }
        #endregion

        #region GUI Methods

        private void OnGUI()
        {
            Event e = Event.current;
            _mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindow();
        }

        private void DrawWindow()
        {
            BeginWindows();
            foreach (BaseNode n in windows)
            {
                n.DrawCurve();
            }

            for (int i = 0; i < windows.Count; i++)
            {
                windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
            }
            
            EndWindows();
        }

        private void DrawNodeWindow(int id)
        {
            windows[id].DrawWindow();
            GUI.DragWindow();
        }

        private void UserInput(Event e)
        {
            if (e.button == 1 && !_makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }
            
            if (e.button == 0 && !_makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    
                }
            }
        }

        private void RightClick(Event e)
        {
            _selectedNode = null;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(e.mousePosition))
                {
                    _clickedOnWindow = true;
                    _selectedNode = windows[i];
                    break;
                }
            }

            if (!_clickedOnWindow)
            {
                AddNewNode(e);
            }
            else
            {
                ModifyNode(e);
            }
        }
        
        private void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add Dialogue"),false, ContextCallback,UserActions.AddDialogue);
            menu.AddItem(new GUIContent("Add Action"), false, ContextCallback, UserActions.AddActionNode);
            menu.AddItem(new GUIContent("Add Comment"),false, ContextCallback,UserActions.CommentNode);
            menu.ShowAsContext();
            e.Use();
        }

        void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            if (_selectedNode is DialogueNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Add Transition"), false, ContextCallback, UserActions.AddTransitionNode);
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.DeleteNode);
            }

            if (_selectedNode is ActionNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Add Transition"), false, ContextCallback, UserActions.AddTransitionNode);
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.DeleteNode);
            }

            if (_selectedNode is CommentNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.DeleteNode);
            }
            
            menu.ShowAsContext();
            e.Use();
        }
        
        private void ContextCallback(object o)
        {
            UserActions a = (UserActions) o;
            switch (a)
            {
                case UserActions.AddDialogue:
                    DialogueNode dialogueNode = CreateInstance<DialogueNode>();
                    dialogueNode.windowRect = new Rect(_mousePosition.x, _mousePosition.y,  200, 300);
                    dialogueNode.windowTitle = "Dialogue";
                    windows.Add(dialogueNode);
                    break;
                case UserActions.AddActionNode:
                    ActionNode actionNode = CreateInstance<ActionNode>();
                    actionNode.windowRect = new Rect(_mousePosition.x, _mousePosition.y, 200, 300);
                    actionNode.windowTitle = "Action";
                    actionNode.dialogueActions =
                        (ActionNode.DialogueActions) EditorGUILayout.EnumPopup("Action:", actionNode.dialogueActions);
                    windows.Add(actionNode);
                    break;
                case UserActions.AddTransitionNode:
                    break;
                case UserActions.CommentNode:
                    CommentNode commentNode = CreateInstance<CommentNode>();
                    commentNode.windowRect = new Rect(_mousePosition.x, _mousePosition.y, 200, 100);
                    commentNode.windowTitle = "Comment";
                    windows.Add(commentNode);
                    break;
                case UserActions.DeleteNode:
                    if (_selectedNode != null)
                    {
                        windows.Remove(_selectedNode);
                    }
                    break;
                default:
                    break;
            }
        }
        
        
        #endregion

        #region Helper Methods



        #endregion
    }
}
