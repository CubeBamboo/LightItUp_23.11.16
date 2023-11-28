using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [System.Obsolete]
    public class RbUILogic : MonoBehaviour
    {
        /*
        private Rigidbody2D rb;
        public PhysicsMaterial2D physicsMaterial;

        private void Awake()
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            if (!TryGetComponent<Collider2D>(out var collider)) Debug.LogError("Collider Not Found!");
        }

        private void Start()
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.sharedMaterial = physicsMaterial;
            RbUIManager.Instance.AllRbUI.Add(rb);
        }
        */
    }
}