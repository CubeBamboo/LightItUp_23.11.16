using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class BGSquare : MonoBehaviour
    {
        private bool isLit;
        private SpriteRenderer spriteRenderer;
        //private Color litColor = new Color(192f/255f, 160f/255f, 96f/255f);    //183, 183, 183
        public static int totalLightness;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isLit)
                return;

            if (other.CompareTag(Common.Constant.PLAYER_TAG))
            {
                isLit = true;
                spriteRenderer.sprite = Core.GameResourcesLoadManager.LitOnSprite;
                totalLightness++;
                //Debug.Log("current totalLightness:" + totalLightness);
                this.enabled = true;
            }
        }


    }
}