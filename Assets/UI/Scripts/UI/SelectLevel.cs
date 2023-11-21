using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SelectLevel : Framework.MonoSingleton<SelectLevel>
    {
        public GameObject mainMenuCanvas;

        public static void SwitchToMainMenu()
        {
            Instance.mainMenuCanvas.SetActive(true);
            Instance.gameObject.SetActive(false);
        }

        public static void EnterLevel1()
        {
            Core.MySceneManager.AsyncLoadSceneWithDelay(Common.Constant.LEVEL_1_SCENE_INDEX, 0.3f, 2f);
        }

        public static void EnterLevel2()
        {
            Core.MySceneManager.AsyncLoadSceneWithDelay(Common.Constant.LEVEL_2_SCENE_INDEX, 0.3f, 2f);
        }

        //private static IEnumerator AsyncLoadScene(int sceneBuildIndex)
        //{
        //    //LoadSceneAsync
        //    AsyncOperation op = SceneManager.LoadSceneAsync(sceneBuildIndex);
        //    op.allowSceneActivation = false;
        //    float delayTime = 0.5f;
        //    yield return new WaitForSeconds(delayTime);

        //    //show animation and delay a several time.
        //    Instance.loadingPanel.SetActive(true);
        //    yield return new WaitForSeconds(1.5f);

        //    op.allowSceneActivation = true;
        //}
    }
}