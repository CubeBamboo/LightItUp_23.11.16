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
            playerController.ResetTrail += ResetTrail;
        }

        private void ResetTrail()
        {
            trailRenderer.Clear();
        }
    }
}