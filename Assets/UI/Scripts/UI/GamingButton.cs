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
            //加载当前场景
            Debug.Log("Retry!");
            Core.MySceneManager.AsyncLoadScene(SceneManager.GetActiveScene().buildIndex);
            //Core.MySceneManager.Instance.F();
        }

        public void BackToMenu()
        {
            //加载主界面场景
            
            Debug.Log("Back To Menu!");

        }

        public void EnterNextLevel()
        {
            //加载下一关场景
            Debug.Log("Enter Next Level!");

        }

        #endregion

        #region MainMenu

        public void StartGame()
        {
            MainMenu.SwitchToSelectLevel();
            Debug.Log("SelectLevel.");
        }

        public void EnterTutorial()
        {
            MainMenu.SetTutorialPanel(true);
            Debug.Log("Enter Tutorial.");
        }

        public void ExitTutorial()
        {
            MainMenu.SetTutorialPanel(false);
            Debug.Log("Exit Tutorial.");
        }

        public void EnterCredits()
        {
            MainMenu.SetCreditsPanel(true);
            Debug.Log("Enter Credits.");
        }

        public void ExitCredits()
        {
            MainMenu.SetCreditsPanel(false);
            Debug.Log("Exit Credits.");
        }

        public void ExitGame()
        {
            Debug.Log("ExitGame.");
            Application.Quit();
        }

        #endregion

        #region SelectLevel

        public void ExitSelectLevelPanel()
        {
            SelectLevel.SwitchToMainMenu();
        }

        public void EnterLevel1()
        {
            SelectLevel.EnterLevel1();
        }

        public void EnterLevel2()
        {
            SelectLevel.EnterLevel2();
        }

        #endregion

    }
}