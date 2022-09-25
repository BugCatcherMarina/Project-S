using System;
using System.Collections.Generic;
using UnityEngine;
using Isamu.Utils;
using Isamu.Map.Navigation;

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
                    Tile tile = CreateTile(x, z);
                    nodes.Add(tile.NavigationNode);
                }
            }
            
            OnMapGenerated?.Invoke(defaultMap, nodes);
        }

        private Tile CreateTile(int x, int z)
        {
            Vector3Int position = new Vector3Int(x, ProjectConsts.TILE_Y_POSITION, z);
            Vector2Int gridPos = new Vector2Int(x, z);

            Tile prefab = GetPrefabForCoordinate(gridPos);
            Tile tile = Instantiate(prefab, position, Quaternion.identity, tileParent);
            tile.Configure(gridPos);
            
            _tiles.Add(tile);
            return tile;
        }

        private Tile GetPrefabForCoordinate(Vector2Int coord)
        {
            int impassableCount = defaultMap.ImpassableTiles.Count;

            Debug.Log($"imp count: {impassableCount}");
            
            if (impassableCount == 0)
            {
                return grassTile;
            }

            for (int i = 0; i < impassableCount; i++)
            {
                Vector2Int impassableCoord = defaultMap.ImpassableTiles[i];
                Debug.Log($"imp coord: {impassableCoord}");
                Debug.Log($"this coord: {coord}");
                if (impassableCoord.x == coord.x && impassableCoord.y == coord.y)
                {
                    return impassableTile;
                }
            }

            return grassTile;
        }
    }
}
