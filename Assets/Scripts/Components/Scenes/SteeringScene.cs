using System;
using UnityEngine;

namespace Assets.Scripts.Components.Scenes
{
    public class SteeringScene : MonoBehaviourSingleton
    {
        [SerializeField]
        protected Vector3 m_Gravity = Physics.gravity;

        public static new SteeringScene Instance
        {
            get { return GetInstance<SteeringScene>(); }
        }

        protected override void Awake()
        {
            base.Awake();

            Physics.gravity = m_Gravity;
        }
    }
}
