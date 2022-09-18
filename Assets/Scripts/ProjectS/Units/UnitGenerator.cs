using System;
using UnityEngine;

namespace ProjectS.Units
{
    public class UnitGenerator : MonoBehaviour
    {
        [SerializeField] private UnitBehaviour unitBehaviourPrefab;

        [SerializeField] private UnitAsset[] units;

        private void Start()
        {
            throw new NotImplementedException();
        }

        private void Generate()
        {
            for (int i = 0, length = units.Length; i< length; i++)
        }
    }
}
