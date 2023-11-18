using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        //public static T instance;
        //protected void Awake()
        //{
        //    instance = GetComponent<T>();
        //}

        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                //Debug.Log("A new Instance!");
            }
        }
    }
}