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

        #region AsyncLoadScene

        public static void AsyncLoadSceneWithFade(Common.SceneIndex sceneBuildIndex)
        {
            if (Instance)
            {
                if (Instance.LoadingCoroutine != null) return;

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
    }
}