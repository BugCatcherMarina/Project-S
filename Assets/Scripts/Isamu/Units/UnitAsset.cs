using Isamu.Utils;
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

        public SpawnPosition Spawn => spawnPosition;
        public UnitStats Stats => stats;
        
        [SerializeField] private SpawnPosition spawnPosition;
        [SerializeField] private UnitStats stats;
    }
}
