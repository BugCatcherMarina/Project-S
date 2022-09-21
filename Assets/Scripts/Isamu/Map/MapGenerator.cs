using System;
using System.Collections.Generic;
using UnityEngine;
using Isamu.Utils;
using Isamu.Map.Navigation;

namespace Isamu.Map
{
    public class MapGenerator : MonoBehaviour
    {


        [SerializeField] private MapAsset defaultMap;
        [SerializeField] private Transform tileParent;
        
        [SerializeField] private Tile grassTile;

        private readonly List<Tile> _tiles = new();

        /// <summary>
        /// A helper function to destroy and recreate the map from the inspector.
        /// </summary>
        [ContextMenu("Recreate Map")]
        public void RecreateMap()
        {
            for (int i = _tiles.Count - 1; i >= 0; i--)
            {
                Destroy(_tiles[i].gameObject);
            }
            
            _tiles.Clear();
            
            Generate();
        }
        
        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            List<NavigationNode> nodes = new List<NavigationNode>();

            for (int x = 0; x < defaultMap.Width; x++)
            {
                for (int z = 0; z < defaultMap.Depth; z++)
                {
                    CreateTile(x, z);
                    nodes.Add(_tiles[_tiles.Count - 1].NavigationNode);
                }
            }
            NavigationGrid.Initialize(new Vector2Int(defaultMap.Width, defaultMap.Depth), nodes);
        }

        private void CreateTile(int x, int z)
        {
            Vector3Int position = new Vector3Int(x, ProjectConsts.TILE_Y_POSITION, z);
            Tile tile = Instantiate(grassTile, position, Quaternion.identity, tileParent);
            tile.Configure(new Vector2Int(x, z));
            _tiles.Add(tile);
        }
    }
}
