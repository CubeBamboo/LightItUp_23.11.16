using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomCamera
{
    /// <summary>
    /// ����������ϣ����������
    /// attach to the GameObject "Camera". It will control the impulse of camera.
    /// </summary>
    public class CameraImpulseSignal : MonoBehaviour
    {
        //listener ���� source �������¼�(��Ϣ)�����յ���Ϣ����д���(�����)
        //�������������ֽ��յ��µ��¼�������Ը����¼�

        public float impulseTime; //��ʱ��
        public float impulseScale; //�𶯷���
        public bool isPlaying { get; private set; }

        public void Impulse()
        {
            if (isPlaying) return; //���� throttle

            StartCoroutine(OnImpulse());
        }

        //���ú�ʵ��һ����
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