using Isamu.Utils;
using Isamu.Units.TurnActions;
using System;
using UnityEngine;

namespace Isamu.Units
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.CUSTOM_ASSET_MENU + NAME)]
    public class UnitAsset : ScriptableObject
    {
        [Serializable]
        public class SpawnPosition
        {
            public int X => x;
            public int Z => z;
            
            [SerializeField, Min(0)] private int x;
            [SerializeField, Min(0)] private int z;
        }
        
        private const string NAME = nameof(UnitAsset);

        public string UnitName => unitName;
        public SpawnPosition Spawn => spawnPosition;
        public UnitStats Stats => stats;
        public ActionAsset[] ActionAssets => actionAssets;
        
        [SerializeField] private string unitName;
        [SerializeField] private SpawnPosition spawnPosition;
        [SerializeField] private UnitStats stats;
        
        [Tooltip("All possible actions this unit can take on its turn.")]
        [SerializeField] private ActionAsset[] actionAssets;
    }
}
