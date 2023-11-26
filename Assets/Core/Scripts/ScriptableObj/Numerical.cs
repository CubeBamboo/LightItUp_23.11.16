using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObj
{
    [CreateAssetMenu(fileName = "new Numerical Data", menuName = "Custom/Numerical")]
    public class Numerical : ScriptableObject
    {
        public float playerInitEnergy;
        public int lightnessGoal;
    }
}