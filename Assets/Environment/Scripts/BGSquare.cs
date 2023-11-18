using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class BGSquare : MonoBehaviour
    {
        private bool isLit;
        private SpriteRenderer spriteRenderer;
        private Color litColor = new Color(183f/255f, 183f/255f, 183f/255f);
        public static int totalLightness;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isLit)
                return;

            if (other.CompareTag(Common.ConstString.PLAYER_TAG))
            {
                isLit = true;
                spriteRenderer.color = litColor;
                totalLightness++;
                //Debug.Log("current totalLightness:" + totalLightness);
                this.enabled = true;
            }
        }


    }
}