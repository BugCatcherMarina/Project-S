using System;
using UnityEngine;

namespace Isamu.Units
{
    [Serializable]
    public class UnitStats
    {
        public int Movement => movement;
        public float Speed => speed;
        
        [Tooltip("How many tiles a unit can move in a turn.")]
        [SerializeField, Min(0)] private int movement;
        
        [Tooltip("Determines turn order. Units with higher speed act first.")]
        [SerializeField, Min(0f)] private float speed;
    }
}
