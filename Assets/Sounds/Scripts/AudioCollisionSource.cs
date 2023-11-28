using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioCollisionSource : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!AudioManager.Instance) return;

            AudioManager.Instance.PlayMenuCollisionSFX();
        }
    }
}