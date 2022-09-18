using System;
using UnityEngine;

namespace Isamu.Units
{
    public class UnitBehaviour : MonoBehaviour
    {
        // Currently unused -- stubbing this in for future use.
        public UnitAsset UnitAsset { get; private set; }
        
        public void Configure(UnitAsset unitAsset)
        {
            UnitAsset = unitAsset;
        }
    }
}
