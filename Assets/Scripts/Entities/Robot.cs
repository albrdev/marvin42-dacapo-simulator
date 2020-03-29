using System;
using UnityEngine;
using Assets.Scripts.Components.UI;

namespace Assets.Scripts.Entities
{
    public class Robot : MonoBehaviour
    {
        [SerializeField]
        private float m_MaxSpeed = 5f;

        [SerializeField]
        private Vector3 m_RotationModifier = Vector3.zero;

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public Quaternion Rotation
        {
            get { return transform.rotation; }
        }

        public void Move(Vector2 direction, Quaternion rotation)
        {
            Vector3 tmpDir = new Vector3(direction.x, direction.y, 0f);
            Quaternion q = rotation * Quaternion.Inverse(Rotation);

            Vector3 globalDir = (rotation * tmpDir).normalized;
            Vector3 localDir = (q * tmpDir).normalized;

            float multiplier = m_MaxSpeed * Time.fixedDeltaTime;
            GetComponent<Rigidbody>().MovePosition(Position + (globalDir * multiplier));

            InfoPanel.Instance.SetValues(direction, globalDir, localDir);
        }

        protected void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                transform.rotation *= Quaternion.Euler(Input.GetKey(KeyCode.LeftShift) ? -m_RotationModifier : m_RotationModifier);
            }
            else if(Input.GetKeyDown(KeyCode.Home))
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
