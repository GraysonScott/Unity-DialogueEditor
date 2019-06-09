using System;
using System.Collections;
using System.Collections.Generic;
using Editor.Nodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor.Nodes
{
    public class ActionNode : BaseNode
    {
        
        public enum DialogueActions
        {
            StartQuest, CompleteQuest, GainFactionRep, LoseFactionRep, GainItem
        }

        public DialogueActions dialogueActions;
     
        public override void DrawWindow()
        {
            
        }

        public override void DrawCurve()
        {
            
        }
    }
}