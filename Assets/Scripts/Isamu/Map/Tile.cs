using System;
using UnityEngine;

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
        private bool _isUnderPointer;

        public int X => GridPosition.x;
        public int Z => GridPosition.y;
        
        public Vector2Int GridPosition { get; private set; }

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
            _isUnderPointer = isTileUnderPointer;
            
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
            name = $"TIle {gridPosition}";
            Material.color = tileDefaults.DefaultColor;
        }
    }
}
