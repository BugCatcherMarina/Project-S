using System;
using System.Collections.Generic;
using UnityEngine;
using Isamu.Utils;
using Isamu.Map.Navigation;
using Isamu.Services;

namespace Isamu.Map
{
    public class MapGenerator : MonoBehaviour
    {
        public static event Action<MapAsset, List<NavigationNode>> OnMapGenerated; 

        [SerializeField] private MapAsset defaultMap;
        [SerializeField] private Transform tileParent;
        
        [SerializeField] private Tile grassTile;
        [SerializeField] private Tile impassableTile;

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
                    Tile tile = CreateTile(x, z, defaultMap);
                    nodes.Add(tile.NavigationNode);
                }
            }
            
            OnMapGenerated?.Invoke(defaultMap, nodes);
        }

        private Tile CreateTile(int x, int z, MapAsset mapData = null)
        {
            Vector3Int position = new Vector3Int(x, ProjectConsts.TILE_Y_POSITION, z);
           
            Tile tile = null;
            
            if (mapData != null)
            {
                foreach (Vector2Int entry in mapData.ImpassableTiles)
                {
                    if (entry != new Vector2Int(x, z))
                    {
                        continue;
                    }

                    tile = Instantiate(impassableTile, position, Quaternion.identity, tileParent);
                    break;
                }
            }
            
            if (tile == null)
            {
                tile = Instantiate(grassTile, position, Quaternion.identity, tileParent);
            }

            tile.Configure(new Vector2Int(x, z));
            _tiles.Add(tile);
            return tile;
        }
    }
}
