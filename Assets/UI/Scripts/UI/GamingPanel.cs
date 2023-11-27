using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GamingPanel : MonoBehaviour
    {
        public UnityEngine.UI.Text scoreText;
        public UnityEngine.UI.Image energyBarBG, energyBarContent;
        private Character.PlayerController player;
        public GameObject dropArrowEffect;

        private void Start()
        {
            player = GameObject.Find(Common.Constant.PLAYER_PATH).GetComponent<Character.PlayerController>();
            player.energyBarEffectAction += DropArrowEffect;
        }

        private void FixedUpdate()
        {
            UpdateScoreText(Environment.BGSquare.totalLightness, Core.GameManager.Instance.lightnessGoal);
            UpdateEnergyBar(player.Energy, player.initEnergy);
        }
        
        private void UpdateScoreText(float currValue, float goalValue)
        {
            scoreText.text = "<color=orange>" + currValue + "</color>" + " / " + goalValue;
        }

        private void UpdateEnergyBar(float currValue, float initValue)
        {
            //BarContentHeight = currEnergy/initEnergy * BarBGHeight
            float scale = currValue / initValue; if(scale > 1f) scale = 1f; else if (scale < -0.1f) scale = -0.1f;
            energyBarContent.rectTransform.sizeDelta = new Vector2(energyBarContent.rectTransform.sizeDelta.x, scale * energyBarBG.rectTransform.sizeDelta.y);
        }

        private void DropArrowEffect()
        {
            dropArrowEffect.SetActive(true);
        }
    }
}