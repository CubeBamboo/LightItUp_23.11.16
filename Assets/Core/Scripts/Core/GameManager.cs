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
            player = GameObject.Find(Common.Constant.PLAYER_PATH).GetComponent<Character.PlayerController>();
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
            StartCoroutine(OnStageClearCoroutie());
        }

        private IEnumerator OnStageClearCoroutie()
        {
            Debug.Log("Stage Clear!");
            Audio.AudioManager.Instance?.PlayStageClearSFX();//sound Logic
            isGameComplete = true;
            player.OnGameComplete();    //playerLogic
            yield return new WaitForSeconds(1.0f);  //delayTime
            stageClearPanel.SetActive(true);    //UI
        }

        private void OnGameFail()
        {
            StartCoroutine(OnGameFailCoroutie());
        }

        private IEnumerator OnGameFailCoroutie()
        {
            Debug.Log("Game Over!");
            Audio.AudioManager.Instance?.PlayGameFailSFX();//sound Logic
            isGameComplete = true;
            player.OnGameComplete();    //playerLogic
            yield return new WaitForSeconds(1.0f);  //delayTime
            gameFailPanel.SetActive(true);  //UI
        }
    }
}