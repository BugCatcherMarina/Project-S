using Isamu.Utils;
using UnityEngine;

namespace Isamu.Units
{
    public class UnitGenerator : MonoBehaviour
    {
        // The units that should be created on load.
        [SerializeField] private UnitAsset[] units;
        
        [SerializeField] private UnitBehaviour unitPrefab;
        [SerializeField] private Transform unitParent;
        
        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            for (int i = 0, length = units.Length; i < length; i++)
            {
                CreateUnit(units[i]);
            }
        }

        private void CreateUnit(UnitAsset unitAsset)
        {
            UnitAsset.SpawnPosition spawn = unitAsset.Spawn;
            Vector3 worldPosition = new Vector3(spawn.X, ProjectConsts.UNIT_Y_POSITION, spawn.Z);
            UnitBehaviour unit = Instantiate(unitPrefab, worldPosition, Quaternion.identity, unitParent);
            unit.Configure(unitAsset);
        }
    }
}
