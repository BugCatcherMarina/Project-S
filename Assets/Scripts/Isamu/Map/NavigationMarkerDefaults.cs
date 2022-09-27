using Isamu.Utils;
using UnityEngine;

namespace Isamu.Map.Navigation
{
    /// <summary>
    /// TileDefaults stores values that all Tiles need, allowing these non-changing values to live in a single place.
    /// </summary>
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.CUSTOM_ASSET_MENU + NAME)]
    public class NavigationMarkerDefaults : ScriptableObject
    {
        private const string NAME = nameof(NavigationMarkerDefaults);

        public Color DefaultColor => defaultColor;
        public Color UnavailableColor => hoverColor;
        public Color DangerColor => selectedColor;
        
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color selectedColor;
    }
}
