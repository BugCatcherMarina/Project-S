using UnityEngine;

namespace Isamu.Units
{
    public class UnitBehaviour : MonoBehaviour
    {
        // Currently unused -- stubbing this in for future use.
        private UnitAsset _unitAsset;
        
        public void Configure(UnitAsset unitAsset)
        {
            _unitAsset = unitAsset;
        }
    }
}
