﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    [DisallowMultipleComponent]
    public abstract class MonoBehaviourSingleton : MonoBehaviour
    {
        private static readonly Dictionary<System.Type, MonoBehaviourSingleton> s_Instances = new Dictionary<System.Type, MonoBehaviourSingleton>();

        protected static IReadOnlyDictionary<System.Type, MonoBehaviourSingleton> Instances { get { return s_Instances; } }

        public static T GetInstance<T>() where T : MonoBehaviourSingleton
        {
            s_Instances.TryGetValue(typeof(T), out MonoBehaviourSingleton instance);
            return (T)instance;
        }

        public static MonoBehaviourSingleton Instance
        {
            get { return null; }
        }

        protected virtual void Awake()
        {
            s_Instances.Add(GetType(), this);
        }

        protected virtual void OnDestroy()
        {
            s_Instances.Remove(GetType());
        }
    }
}
