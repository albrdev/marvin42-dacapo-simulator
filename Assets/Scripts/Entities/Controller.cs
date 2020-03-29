using System;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_RotationModifier = Vector3.zero;

        [SerializeField]
        private Robot m_Robot = null;

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public Quaternion Rotation
        {
            get { return transform.rotation; }
        }

        protected virtual void HandleInput()
        {
            Vector2 input;
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input != Vector2.zero)
            {
                m_Robot.Move(input, Rotation);
            }
        }

        protected void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                transform.rotation *= Quaternion.Euler(Input.GetKey(KeyCode.LeftShift) ? -m_RotationModifier : m_RotationModifier);
            }
            else if(Input.GetKeyDown(KeyCode.End))
            {
                transform.rotation = Quaternion.identity;
            }
        }

        protected void FixedUpdate()
        {
            HandleInput();
        }
    }
}
