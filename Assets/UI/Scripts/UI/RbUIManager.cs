using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class RbUIManager : Framework.MonoSingleton<RbUIManager>
    {
        public Character.PlayerController player;
        public GameObject selectLevelCanvas;
        public GameObject rbCanvasPrefab;
        

        protected override void Awake()
        {
            base.Awake();
        }

        public static void EventHappened()
        {
            OnEventHappened();
        }

        public static void EventHappened(string name)
        {
            var canvas = OnEventHappened();
            canvas.GetComponent<RbCanvas>().SetLevelButton(name);
        }

        private static GameObject OnEventHappened()
        {
            //Debug.Log("RbUI Event.");
            //hide select Level UI
            Instance.selectLevelCanvas.SetActive(false);
            //Instantiate rbCanvas prefab
            var canvas = Instantiate(Instance.rbCanvasPrefab);
            //destroy player
            Instance.player.OnGameComplete(); Instance.player.OnPlayerDie();

            return canvas;
        }
    }
}