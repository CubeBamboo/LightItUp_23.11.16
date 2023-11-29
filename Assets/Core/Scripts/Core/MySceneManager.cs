using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class MySceneManager : Framework.MonoSingleton<MySceneManager>
    {
        public GameObject loadingPanelEnter;
        public GameObject loadingPanelExit;
        public static Common.SceneIndex activeSceneIndex => (Common.SceneIndex)SceneManager.GetActiveScene().buildIndex;

        public Coroutine LoadingCoroutine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        //private void Start()
        //{
        //    //Debug.Log("Camera.current:" + Camera.current);
        //    gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        //}

        #region AsyncLoadScene

        public static void AsyncLoadSceneWithFade(Common.SceneIndex sceneBuildIndex)
        {
            if (Instance)
            {
                if (Instance.LoadingCoroutine != null) return; //加载协程正在运行 LoadingCoroutine's throttle. 

                Instance.LoadingCoroutine = Instance.StartCoroutine(pAsyncLoadSceneWithFade((int)sceneBuildIndex));
            }
            else
            {
                SceneManager.LoadSceneAsync((int)sceneBuildIndex);
            }
        }

        private static IEnumerator pAsyncLoadSceneWithFade(int sceneBuildIndex)
        {
            //LoadSceneAsync
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneBuildIndex);
            op.allowSceneActivation = false;

            //enter loadingPanel
            Instance.loadingPanelEnter.SetActive(true);
            yield return new WaitForSeconds(1.5f);  //wait for the animtion

            op.allowSceneActivation = true;
            Instance.loadingPanelExit.SetActive(true);
            Instance.loadingPanelEnter.SetActive(false);
            Instance.LoadingCoroutine = null;   //TODO: is it necessary ?
        }

        #endregion

        #region EnterLevel

        public static void EnterLevel(Common.SceneIndex sceneIndex)
        {
            Instance.StartCoroutine(ToEnterLevel(sceneIndex));
        }

        private static IEnumerator ToEnterLevel(Common.SceneIndex sceneIndex)
        {
            Debug.Log("startEnterLevel Coroutine.");
            yield return new WaitForSeconds(4.0f); //timeDelay
            //Debug.Log("endEnterLevel Coroutine.");
            AsyncLoadSceneWithFade(sceneIndex);

        }

        #endregion
    }
}