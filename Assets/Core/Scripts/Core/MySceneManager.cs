using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class MySceneManager : Framework.MonoSingleton<MySceneManager>
    {
        public GameObject loadingPanelEnter;

        public static Coroutine AsyncLoadScene(int sceneBuildIndex)
        {
            return Instance.StartCoroutine(m_AsyncLoadScene(sceneBuildIndex));
        }

        private static IEnumerator m_AsyncLoadScene(int sceneBuildIndex)
        {
            Debug.Log("AsyncLoadScene");
            //LoadSceneAsync
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneBuildIndex);
            op.allowSceneActivation = false;

            //show animation
            Instance.loadingPanelEnter.SetActive(true);  //TODO: where is loadingPanelExit ?
            yield return new WaitForSeconds(0.8f);  //wait for the animtion

            op.allowSceneActivation = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sceneBuildIndex"></param>
        /// <param name="delayTimeBefore"> Delay Time before show the Loading Panel. </param>
        /// <param name="delayTimeAfter"> Delay Time after show the Loading Panel.</param>
        /// <returns></returns>
        public static Coroutine AsyncLoadSceneWithDelay(int sceneBuildIndex, float delayTimeBefore, float delayTimeAfter)
        {
            return Instance.StartCoroutine(m_AsyncLoadSceneWithDelay(sceneBuildIndex, delayTimeBefore, delayTimeAfter));
        }

        private static IEnumerator m_AsyncLoadSceneWithDelay(int sceneBuildIndex, float delayTimeBefore, float delayTimeAfter)
        {
            //LoadSceneAsync
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneBuildIndex);
            op.allowSceneActivation = false;
            //delay
            yield return new WaitForSeconds(delayTimeBefore);

            //show animation
            Instance.loadingPanelEnter.SetActive(true);  //TODO: where is loadingPanelExit ?
            yield return new WaitForSeconds(delayTimeAfter);  //wait for the animtion

            op.allowSceneActivation = true;
        }
    }
}