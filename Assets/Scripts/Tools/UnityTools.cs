using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Tools
{
    public static class UnityTools
    {
        public static class Input
        {
            public static bool CursorOverlapsScreen()
            {
                return (UnityEngine.Input.mousePosition.x >= 0f && UnityEngine.Input.mousePosition.x < Screen.width) && (UnityEngine.Input.mousePosition.y >= 0f && UnityEngine.Input.mousePosition.y < Screen.height);
            }

            public static bool CursorOverlapsUI()
            {
                return GUIUtility.hotControl != 0 || (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject());
            }
        }
    }
}
