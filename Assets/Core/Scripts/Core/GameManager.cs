using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameManager : Framework.MonoSingleton<GameManager>
    {
        //[Header("MonoGameObject")]
        private Character.PlayerController player;
        
        [Header("Level Related")]
        public ScriptableObj.Numerical levelData;
        public float lightnessGoal => levelData.lightnessGoal;

        [Header("UIPanel")]
        public GameObject gameFailPanel;
        public GameObject stageClearPanel;
        private bool isGameComplete;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            Debug.Assert(levelData != null);
            player = GameObject.FindWithTag(Common.Constant.PLAYER_TAG).GetComponent<Character.PlayerController>();
            //init
            Environment.BGSquare.totalLightness = 0;
        }

        private void FixedUpdate()
        {
            GameOverDetect(Environment.BGSquare.totalLightness, player.Energy);
        }

        //it's shit... who created it
        public void GameOverDetect(int currLightness, float currEnergy)
        {
            //if (currEnergy > 0f)
            //    return;
            if (isGameComplete)
                return;

            //Debug.Log("GameManager:" + currEnergy);

            if(currLightness >= lightnessGoal)
            {
                OnStageClear();
            }

            if (currEnergy < 0f)
            {
                OnGameFail();
            }
        }

        private void OnStageClear()
        {
            Debug.Log("Stage Clear!");
            stageClearPanel.SetActive(true);    //UI
            isGameComplete = true;
            player.OnGameComplete();    //playerLogic
            Audio.AudioManager.Instance?.PlayStageClearSFX();//sound Logic
        }

        private void OnGameFail()
        {
            Debug.Log("Game Over!");
            gameFailPanel.SetActive(true);  //UI
            isGameComplete = true;
            player.OnGameComplete();    //playerLogic
            Audio.AudioManager.Instance?.PlayGameFailSFX();//sound Logic
        }
    }
}