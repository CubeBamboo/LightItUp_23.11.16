using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomCamera
{
    /// <summary>
    /// 挂载在相机上，控制相机震动
    /// attach to the GameObject "Camera". It will control the impulse of camera.
    /// </summary>
    public class CameraImpulseSignal : MonoBehaviour
    {
        //listener 监听 source 发来的事件(消息)，接收到消息后进行处理(相机震动)
        //如果处理过程中又接收到新的事件，则忽略该新事件

        public float impulseTime; //震动时间
        public float impulseScale; //震动幅度
        public bool isPlaying { get; private set; }

        public void Impulse()
        {
            if (isPlaying) return; //节流 throttle

            StartCoroutine(OnImpulse());
        }

        //调用后实现一个震动
        private IEnumerator OnImpulse()
        {
            Debug.Log("Camera Impulse");
            isPlaying = true;
            Vector3 initPos = transform.position;
            Vector2 randomPos = impulseScale * (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
            //transform.position = new Vector3(randomPos.x, randomPos.y, initPos.z);
            
            yield return new WaitForSeconds(impulseTime);

            transform.position = new Vector3(randomPos.x, randomPos.y, initPos.z);

            yield return new WaitForSeconds(impulseTime);
            transform.position = initPos;
            isPlaying = false;
        }
    }   
}