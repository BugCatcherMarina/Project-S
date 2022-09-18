using System;
using UnityEngine;

namespace Isamu.Map
{
    public class Tile : MonoBehaviour
    {
        public static event Action<Tile> OnClick;
        
        [SerializeField] private TileDefaults tileDefaults;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private bool isSelected;
        
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
        
        public void IsHovering(bool isHover)
        {
            if (!isSelected)
            {
                Material.color = isHover ? tileDefaults.HoverColor : tileDefaults.DefaultColor;
            }
        }

        public void SetIsSelected(bool isTileSelected)
        {
            isSelected = isTileSelected;
            Material.color = isTileSelected ? tileDefaults.SelectedColor : tileDefaults.DefaultColor;
        }

        private void Start()
        {
            Material.color = tileDefaults.DefaultColor;
        }
    }
}
