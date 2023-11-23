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
            Core.MySceneManager.AsyncLoadSceneWithFade(Common.SceneIndex.LEVEL_1);
        }

        public static void EnterLevel2()
        {
            Core.MySceneManager.AsyncLoadSceneWithFade(Common.SceneIndex.LEVEL_2);
        }
    }
}