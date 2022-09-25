using System.Linq;
using System.Collections.Generic;
using Isamu.Utils;
using UnityEngine;

namespace Isamu.Map
{
    /// <summary>
    /// MapAsset currently stores the grid dimensions, but could be expanded on later.
    /// </summary>
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.CUSTOM_ASSET_MENU + NAME)]
    public class MapAsset : ScriptableObject
    {
        private const string NAME = nameof(MapAsset);

        public int Width => width;
        public int Depth => depth;
        
        public List<Vector2Int> ImpassableTiles => impassableTiles;

        [Tooltip("The number of tiles along the X axis.")]
        [SerializeField, Min(1)] private int width = 1;
        
        [Tooltip("The number of tiles along the Z axis.")]
        [SerializeField, Min(1)] private int depth = 1;


        [Tooltip("Determenes if unit can walk over the tile.")]
        [SerializeField] private List<Vector2Int> impassableTiles;

        private void OnValidate()
        {
            for (int i = 0; i < impassableTiles.Count; i++) 
            {
                Vector2Int tile = impassableTiles[i];
                if (tile.x < 0) tile.x = 0;
                if (tile.y < 0) tile.y = 0;
                if (tile.x >= width) tile.x = width -1;
                if(tile.y >= depth) tile.y = depth -1;
            }
            //impassableTiles = impassableTiles.Distinct().ToList();
        }
    }
}
