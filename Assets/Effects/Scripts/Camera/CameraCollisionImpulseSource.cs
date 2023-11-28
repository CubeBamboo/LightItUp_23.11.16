using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomCamera
{
    /// <summary>
    /// ·¢ËÍÊÂ¼þ
    /// </summary>
    public class CameraCollisionImpulseSource : MonoBehaviour
    {
        public static System.Action Notify;

        private void OnCollisionEnter2D(Collision2D other)
        {
            //Debug.Log("Collision Hit");
            Notify?.Invoke();
        }

        //private void Update()
        //{
        //    var keyboard = UnityEngine.InputSystem.Keyboard.current;
        //    if (keyboard.vKey.wasPressedThisFrame)
        //        Notify?.Invoke();

        //}
    }
}