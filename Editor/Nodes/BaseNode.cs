using UnityEngine;

namespace Editor.Nodes
{
    public abstract class BaseNode : ScriptableObject
    {
        public GUIStyle windowStyle;
        public Rect windowRect;
        public string windowTitle;
        
        public virtual void DrawWindow()
        {
            
        }

        public virtual void DrawCurve()
        {
            
        }
    }
}

