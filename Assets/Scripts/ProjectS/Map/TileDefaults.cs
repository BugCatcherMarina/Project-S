using ProjectS.Utils;
using UnityEngine;

namespace ProjectS.Map
{
    /// <summary>
    /// TileDefaults stores values that all Tiles need, allowing these non-changing values to live in a single place.
    /// </summary>
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.CUSTOM_ASSET_MENU + NAME)]
    public class TileDefaults : ScriptableObject
    {
        private const string NAME = nameof(TileDefaults);

        public Color DefaultColor => defaultColor;
        public Color HoverColor => hoverColor;
        public Color SelectedColor => selectedColor;
        
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color selectedColor;
    }
}
