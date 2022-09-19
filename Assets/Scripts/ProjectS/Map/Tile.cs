using System;
using UnityEngine;

namespace ProjectS.Map
{
    public class Tile : MonoBehaviour
    {
        

        public static event Action<Tile> OnClick;
        public static event Action<Tile> OnPointerEnter;
        public static event Action<Tile> OnPointerExit;

        [SerializeField] private TileDefaults tileDefaults;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private bool isSelected;
        private bool isUnderPointer;

        Vector2Int _gridPosition = Vector2Int.one * -1;
        public Vector2Int GridPosition { 
            get {
                return _gridPosition; 
             }
            set { 
                _gridPosition = value;
            }
        }
        
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
            isUnderPointer = isTileUnderPointer;
            if (!isSelected)
            {
                Material.color = isTileUnderPointer ? tileDefaults.HoverColor : tileDefaults.DefaultColor;
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
            name = "Tile " + GridPosition.ToString();
        }
    }
}