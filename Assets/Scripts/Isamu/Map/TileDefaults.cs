using Isamu.Utils;
using UnityEngine;

namespace Isamu.Map
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
        public Color AvailableColor => availableColor;
        public Color UnavailableColor => unavailableColor;
        public Color RiskyColor => riskyColor;
        public Color ImmenentDangerColor => immenentDangerColor;

        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color availableColor;
        [SerializeField] private Color unavailableColor;
        [SerializeField] private Color riskyColor;
        [SerializeField] private Color immenentDangerColor;
    }

}
