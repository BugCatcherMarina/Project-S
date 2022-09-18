using System;
using ProjectS.Utils;
using UnityEngine;

namespace ProjectS.Units
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
        
        [SerializeField] private SpawnPosition spawnPosition;
    }
}
