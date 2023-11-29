using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameResourcesLoadManager
    {
        private static Sprite litOnSprite;
        public static Sprite LitOnSprite
        {
            get
            {
                if (litOnSprite == null)
                {
                    litOnSprite = Resources.Load<Sprite>(Common.Constant.LIT_ON_BLOCK_RES_PATH);
                    if (litOnSprite == null) Debug.LogError("litOnSprite Resources Load Error!");
                    //else Debug.Log("litOnSprite Resources Load correctly.");
                }

                return litOnSprite;
            }
        }

    }
}