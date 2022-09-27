using System;
using UnityEngine;
using Isamu.Map.Navigation;

namespace Isamu.Map
{
    public class Tile : MonoBehaviour
    {
        public static event Action<Tile> OnClick;
        public static event Action<Tile> OnPointerEnter;
        public static event Action<Tile> OnPointerExit;

        [SerializeField] private TileDefaults tileDefaults;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private bool _isSelected;
        
        public Vector2Int GridPosition { get; private set; }

        public NavigationNode NavigationNode => navigationNode;
        [SerializeField] private NavigationNode navigationNode;

        private Material Material
        {
            get
            {
                _material ??= meshRenderer.material;
                return _material;
            }
        }
        
        private Material _material;

        public void HandlePointerClick()
        {
            OnClick?.Invoke(this);
        }
        public void HandlePointerEnter()
        {
            OnPointerEnter?.Invoke(this);
        }
        public void HandlePointerExit()
        {
            OnPointerExit?.Invoke(this);
        }

        public void SetIsUnderPointer(bool isTileUnderPointer)
        {
            if (!_isSelected)
            {
                Material.color = isTileUnderPointer ? tileDefaults.HoverColor : tileDefaults.DefaultColor;
            }
        }

        public void SetIsSelected(bool isTileSelected)
        {
            _isSelected = isTileSelected;
            Material.color = isTileSelected ? tileDefaults.SelectedColor : tileDefaults.DefaultColor;
        }

        public void Configure(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
            name = $"Tile {gridPosition}";
            Material.color = tileDefaults.DefaultColor;
        }
    }
}
