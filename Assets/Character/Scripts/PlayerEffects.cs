using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class PlayerEffects : MonoBehaviour
    {
        private PlayerController playerController;
        private TrailRenderer trailRenderer;
        
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        private void Start()
        {
            playerController.ResetTrailAction += ResetTrail;
            playerController.soundEffectAction += PlayMoveSound;
        }

        private void ResetTrail()
        {
            trailRenderer?.Clear();
        }

        private void PlayMoveSound()
        {
            Audio.AudioManager.Instance?.PlayPlayerMoveSFX();
        }
    }
}