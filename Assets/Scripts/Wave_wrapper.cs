using System;
using model;
using UnityEngine;

namespace DefaultNamespace
{
    public class Wave_wrapper : MonoBehaviour
    {
        public int level1Amount;
        public int level2Amount;
        public int level3Amount;
        public float rateOfSpawn;

        public Wave Wave { get; set; }

        private void Awake()
        {
            Wave = new Wave(level1Amount, level2Amount, level3Amount, rateOfSpawn);
        }
    }
}