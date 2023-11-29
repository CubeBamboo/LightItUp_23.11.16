using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GamingButton : MonoBehaviour
    {
        #region GameOverPanel

        public void Retry()
        {
            //Debug.Log("Retry!");
            Core.MySceneManager.AsyncLoadSceneWithFade(Core.MySceneManager.activeSceneIndex);
        }

        public void BackToMenu()
        {
            //Debug.Log("Back To Menu!");
            Core.MySceneManager.AsyncLoadSceneWithFade(Common.SceneIndex.MAIN_MENU);
        }

        public void EnterNextLevel()
        {
            //Debug.Log("Enter Next Level!");
            Common.SceneIndex nextLevelIndex = Core.MySceneManager.activeSceneIndex + 1;
            nextLevelIndex = nextLevelIndex < Common.SceneIndex.LEVEL_OUT_OF_RANGE ? nextLevelIndex : Common.SceneIndex.DEMO_END;
            Core.MySceneManager.AsyncLoadSceneWithFade(nextLevelIndex);
        }

        #endregion

        #region MainMenu

        public void StartGame()
        {
            //Debug.Log("SelectLevel.");
            MainMenu.SwitchToSelectLevel();
        }

        public void EnterTutorial()
        {
            //Debug.Log("Enter Tutorial.");
            MainMenu.SetTutorialPanel(true);
        }

        public void ExitTutorial()
        {
            //Debug.Log("Exit Tutorial.");
            MainMenu.SetTutorialPanel(false);
        }

        public void EnterCredits()
        {
            //Debug.Log("Enter Credits.");
            MainMenu.SetCreditsPanel(true);
        }

        public void ExitCredits()
        {
            //Debug.Log("Exit Credits.");
            MainMenu.SetCreditsPanel(false);
        }

        public void ExitGame()
        {
            //Debug.Log("ExitGame.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

#endregion

        #region SelectLevel

        public void ExitSelectLevelPanel()
        {
            SelectLevel.SwitchToMainMenu();
        }

        public void EnterLevel1()
        {
            Core.MySceneManager.EnterLevel(Common.SceneIndex.LEVEL_1);
        }

        public void EnterLevel2()
        {
            Core.MySceneManager.EnterLevel(Common.SceneIndex.LEVEL_2);
        }

        public void EnterLevel3()
        {
            Core.MySceneManager.EnterLevel(Common.SceneIndex.LEVEL_3);
        }

        #endregion

    }
}