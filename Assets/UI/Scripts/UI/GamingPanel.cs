using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GamingPanel : MonoBehaviour
    {
        public UnityEngine.UI.Text scoreText;
        public UnityEngine.UI.Image energyBarBG, energyBarContent;
        public Character.PlayerController player;

        private void FixedUpdate()
        {
            UpdateScoreText();
            UpdateEnergyBar();
        }
        
        private void UpdateScoreText()
        {
            scoreText.text = "<color=orange>" + Environment.BGSquare.totalLightness + "</color>" + " / " + Core.GameManager.Instance.lightnessGoal;
        }

        private void UpdateEnergyBar()
        {
            //BarContentHeight = currEnergy/initEnergy * BarBGHeight
            float scale = player.Energy / player.initEnergy; if(scale > 1f) scale = 1f; else if (scale < -0.1f) scale = -0.1f;
            energyBarContent.rectTransform.sizeDelta = new Vector2(energyBarContent.rectTransform.sizeDelta.x, scale * energyBarBG.rectTransform.sizeDelta.y);
        }
    }
}