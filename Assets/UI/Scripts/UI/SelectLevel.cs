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
    }
}