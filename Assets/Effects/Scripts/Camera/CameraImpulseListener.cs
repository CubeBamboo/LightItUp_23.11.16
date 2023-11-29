using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomCamera
{
    /// <summary>
    /// ¼àÌýÊÂ¼þ
    /// </summary>
    [RequireComponent(typeof(CameraImpulseSignal))]
    public class CameraImpulseListener : MonoBehaviour
    {
        private CameraImpulseSignal signal;

        private void Awake()
        {
            signal = GetComponent<CameraImpulseSignal>();
        }

        private void Start()
        {
            CameraCollisionImpulseSource.Notify += OnNotify;
        }

        private void OnNotify()
        {
            signal.Impulse();
        }

        private void OnDestroy()
        {
            CameraCollisionImpulseSource.Notify -= OnNotify;
        }
    }
}