using System;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Components
{
    [Flags]
    public enum Axis : byte
    {
        None = 0,
        X = (1 << 0),
        Y = (1 << 1),
        Z = (1 << 2),
        W = (1 << 3),
    };

    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        protected float m_MinZoom = 1f;
        [SerializeField]
        protected float m_MaxZoom = 10f;
        [SerializeField]
        protected float m_ZoomSensitivity = 1f;
        [SerializeField]
        protected string m_ZoomAxisName = string.Empty;
        [SerializeField]
        protected bool m_InvertZoomAxis = true;

        [SerializeField]
        protected Vector2 m_PanSensitivity = new Vector2(1f, 1f);
        [SerializeField]
        protected string m_PanButtonName = string.Empty;
        [SerializeField]
        protected string m_CursorHorizontalAxisName = string.Empty;
        [SerializeField]
        protected string m_CursorVerticalAxisName = string.Empty;
        [SerializeField]
        protected bool m_InvertHorizontalPan = true;
        [SerializeField]
        protected bool m_InvertVerticalPan = true;

        [SerializeField]
        protected string m_ResetPanButtonName = string.Empty;

        [SerializeField]
        protected string m_ResetZoomButtonName = string.Empty;

        [SerializeField]
        protected Transform m_FollowTransform = null;
        [SerializeField]
        protected int m_FollowIgnoreAxis = (int)Axis.None;

        protected Camera m_Camera;
        protected Vector3 m_InitialPosition;
        protected float m_InitialZoom;

        public Transform FollowTransform
        {
            get { return m_FollowTransform; }
            set { m_FollowTransform = value; }
        }

        protected virtual void Awake()
        {
            m_Camera = this.GetComponent<Camera>();
            m_InitialPosition = m_Camera.transform.position;
            m_InitialZoom = m_Camera.orthographicSize;
        }

        protected virtual void Update()
        {
            if(!UnityTools.Input.CursorOverlapsScreen() || UnityTools.Input.CursorOverlapsUI())
                return;

            if(m_PanButtonName != string.Empty && Input.GetButton(m_PanButtonName))
            {
                Vector3 position = Vector2.zero;

                if(m_CursorHorizontalAxisName != string.Empty)
                {
                    position.x = Input.GetAxis(m_CursorHorizontalAxisName) * (m_InvertHorizontalPan ? -1f : 1f) * m_PanSensitivity.x;
                }

                if(m_CursorVerticalAxisName != string.Empty)
                {
                    position.y = Input.GetAxis(m_CursorVerticalAxisName) * (m_InvertVerticalPan ? -1f : 1f) * m_PanSensitivity.y;
                }

                m_Camera.transform.position += position;
            }

            if(m_ZoomAxisName != string.Empty)
            {
                float scrollValue = Input.GetAxisRaw(m_ZoomAxisName) * (m_InvertZoomAxis ? -1f : 1f);
                if(scrollValue != 0f)
                {
                    scrollValue = Mathf.Clamp(m_Camera.orthographicSize + (scrollValue * m_ZoomSensitivity), m_MinZoom, m_MaxZoom);
                    m_Camera.orthographicSize = scrollValue;
                }
            }

            if(m_ResetPanButtonName != string.Empty && Input.GetButtonDown(m_ResetPanButtonName))
            {
                m_Camera.transform.position = m_InitialPosition;
            }

            if(m_ResetZoomButtonName != string.Empty && Input.GetButtonDown(m_ResetZoomButtonName))
            {
                m_Camera.orthographicSize = m_InitialZoom;
            }

            if(m_FollowTransform != null)
            {
                m_Camera.transform.position = new Vector3((m_FollowIgnoreAxis & (int)Axis.X) != 0 ? m_Camera.transform.position.x : m_FollowTransform.position.x, (m_FollowIgnoreAxis & (int)Axis.Y) != 0 ? m_Camera.transform.position.y : m_FollowTransform.position.y, (m_FollowIgnoreAxis & (int)Axis.Z) != 0 ? m_Camera.transform.position.z : m_FollowTransform.position.z);
            }
        }
    }
}
