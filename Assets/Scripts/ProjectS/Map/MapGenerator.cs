using System.Collections.Generic;
using UnityEngine;

namespace ProjectS.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapAsset defaultMap;
        [SerializeField] private Transform tileParent;
        
        [SerializeField] private Tile grassTile;

        private readonly List<Tile> tiles = new();

        /// <summary>
        /// A helper function to destroy and recreate the map from the inspector.
        /// </summary>
        [ContextMenu("Recreate Map")]
        public void RecreateMap()
        {
            for (int i = tiles.Count - 1; i >= 0; i--)
            {
                Destroy(tiles[i].gameObject);
            }
            
            tiles.Clear();
            
            Generate();
        }
        
        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            for (int x = 0; x < defaultMap.Width; x++)
            {
                for (int z = 0; z < defaultMap.Depth; z++)
                {
                    CreateTile(x, z);
                }
            }
        }

        private void CreateTile(int x, int z)
        {
            Vector3Int position = new Vector3Int(x, 0, z);
            Tile tile = Instantiate(grassTile, position, Quaternion.identity, tileParent);
            tiles.Add(tile);
        }
    }
}
