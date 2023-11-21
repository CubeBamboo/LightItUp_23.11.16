using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class CommonMonoFunction : MonoBehaviour
    {
        public void DestroyItself()
        {
            Destroy(gameObject);
        }

        public void InactiveItself()
        {
            gameObject.SetActive(false);
        }
    }
}