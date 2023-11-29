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

        public float virboTime; //��ʱ�� how long the vibration will spend
        public int vibroFrequency; //��Ƶ�� how many times it will vibro once
        public float vibroScale; //�𶯷���
        public bool isPlaying { get; private set; }

        public void Impulse()
        {
            if (isPlaying) return; //���� throttle

            StartCoroutine(OnImpulse());
        }

        /*
        //���ú�ʵ��һ����
        private IEnumerator OnImpulse()
        {
            Debug.Log("Camera Impulse");
            isPlaying = true;
            //float partInterval = impulseTime / impulseFrequency;
            Vector3 initPos = transform.position;
            Vector2 randomPos = impulseScale * (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
            //transform.position = new Vector3(randomPos.x, randomPos.y, initPos.z);
            
            //yield return new WaitForSeconds(impulseTime);

            transform.position = new Vector3(randomPos.x, randomPos.y, initPos.z);

            yield return new WaitForSeconds(impulseTime);
            transform.position = initPos;
            isPlaying = false;
        }
        */

        private IEnumerator OnImpulse()
        {
            Debug.Log("Camera Impulse");
            isPlaying = true;
            float partInterval = virboTime / vibroFrequency;
            Vector3 initPos = transform.position;

            for(int i = 0; i < vibroFrequency; i++)
            {
                Vector2 randomPos = vibroScale * (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
                transform.position = new Vector3(randomPos.x, randomPos.y, initPos.z);
                yield return new WaitForSeconds(partInterval);
            }

            transform.position = initPos;
            isPlaying = false;
        }
    }   
}