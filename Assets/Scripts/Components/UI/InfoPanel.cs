using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI
{
    public class InfoPanel : MonoBehaviourSingleton
    {
        public static new InfoPanel Instance
        {
            get { return GetInstance<InfoPanel>(); }
        }

        public Text InputText;
        public Text GlobalText;
        public Text LocalText;

        public void SetValues(Vector2 input, Vector3 global, Vector3 local)
        {
            InputText.text = $@"{input.x:n2}, {input.y:n2}";
            GlobalText.text = $@"{global.x:n2}, {global.y:n2}";
            LocalText.text = $@"{local.x:n2}, {local.y:n2}";
        }
    }
}
