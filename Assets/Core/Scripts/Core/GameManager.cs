using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameManager : Framework.MonoSingleton<GameManager>
    {   
        [Header("MonoGameObject")]
        public Character.PlayerController player;
        
        [Header("Level Related")]
        public float lightnessGoal;

        [Header("UIPanel")]
        public GameObject gameOverPanel;
        public GameObject stageClearPanel;
        private bool isGameComplete;

        private void FixedUpdate()
        {
            GameOverDetect(Environment.BGSquare.totalLightness, player.Energy);
        }

        public void GameOverDetect(int currLightness, float currEnergy)
        {
            //if (currEnergy > 0f)
            //    return;
            if (isGameComplete)
                return;

            //Debug.Log("GameManager:" + currEnergy);

            if(currLightness > lightnessGoal)
            {
                Debug.Log("Stage Clear!");
                stageClearPanel.SetActive(true);
                isGameComplete = true;
                player.OnGameComplete();
            }

            if (currEnergy < 0f)
            {
                Debug.Log("Game Over!");
                gameOverPanel.SetActive(true);
                isGameComplete = true;
                player.OnGameComplete();
            }
        }
    }
}