using System.Collections;
using System.Collections.Generic;
using Editor.Nodes;
using UnityEngine;

namespace Editor.Nodes
{
    public class CommentNode : BaseNode
    {
        private string _comment = "";

        public override void DrawWindow()
        {
            _comment = GUILayout.TextArea(_comment, 200);
        }
    }
}