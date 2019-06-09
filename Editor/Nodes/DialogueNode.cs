using UnityEngine;

namespace Editor.Nodes
{
    public class DialogueNode : BaseNode
    {
        private string _speaker = "";
        private string _dialogue = "";
        
        public override void DrawWindow()
        {
            _speaker = GUILayout.TextField(_speaker, 100);
            _dialogue = GUILayout.TextArea(_dialogue, 200);
        }

        public override void DrawCurve()
        {
            
        }
    }
}
