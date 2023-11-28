using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class RbUIManager : Framework.MonoSingleton<RbUIManager>
    {
        //public List<Rigidbody2D> AllRbUI;
        //public GameObject UICollider;
        public Character.PlayerController player;
        //public PhysicsMaterial2D physicsMaterial;
        public GameObject selectLevelCanvas;
        public GameObject rbCanvasPrefab;

        protected override void Awake()
        {
            base.Awake();
            //AllRbUI = new List<Rigidbody2D>();
        }

        /*
        [System.Obsolete]
        public static void OnEventHappenedOld(Rigidbody2D it)
        {
            Debug.Log("RbUI Event.");
            Instance.AllRbUI.Remove(it);

            //dynamic rb
            foreach (var rbUI in Instance.AllRbUI)
            {
                rbUI.bodyType = RigidbodyType2D.Dynamic;
            }

            //inactive button

            //UI & player Collider
            Instance.UICollider.SetActive(true);
            if (Instance.playerColl)
            {
                Instance.playerColl.isTrigger = false;
                Instance.playerColl.sharedMaterial = Instance.physicsMaterial;
            }
        }
        */

        public static void OnEventHappened()
        {
            //Debug.Log("RbUI Event.");
            //Òþ²Øselect Level UI
            Instance.selectLevelCanvas.SetActive(false);
            //Éú³ÉrbCanvas prefab
            var canvas = Instantiate(Instance.rbCanvasPrefab);
            //Ïú»Ùplayer
            Instance.player.OnGameComplete(); Instance.player.OnPlayerDie();
        }
    }
}