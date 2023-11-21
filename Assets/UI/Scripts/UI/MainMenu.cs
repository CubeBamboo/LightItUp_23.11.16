using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MainMenu : Framework.MonoSingleton<MainMenu>
    {
        public GameObject tutorialPanel;
        public GameObject creditsPanel;
        public GameObject selectLevelCanvas;

        public static void SetTutorialPanel(bool value)
        {
            Instance.tutorialPanel.SetActive(value);
        }

        public static void SetCreditsPanel(bool value)
        {
            Instance.creditsPanel.SetActive(value);
        }

        public static void SwitchToSelectLevel()
        {
            Instance.selectLevelCanvas.SetActive(true);
            Instance.gameObject.SetActive(false);
        }
    }
}