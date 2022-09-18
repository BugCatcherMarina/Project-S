using ProjectS.Utils;
using UnityEngine;

namespace ProjectS.Map
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
        
        [Tooltip("The number of tiles along the X axis.")]
        [SerializeField, Min(1)] private int width = 1;
        
        [Tooltip("The number of tiles along the Z axis.")]
        [SerializeField, Min(1)] private int depth = 1;
    }
}
