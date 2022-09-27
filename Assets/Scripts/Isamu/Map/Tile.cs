using System;
using UnityEngine;
using Isamu.Map.Navigation;

namespace Isamu.Map
{
    public enum TileStates
    {
        Default,
        Available,
        Unavailable,
        Risky,
        ImmenentDanger,
        Source
    }
    
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

        private bool _isUnderPointer;

        private TileStates _state = TileStates.Default;
        
        public TileStates State
        {
            set { _state = value; }
            get { return _state; }
        }
        
        private Vector2Int _gridPosition = Vector2Int.one * -1;
        
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


        private Color PickHoverColor()
        {
            Color color = tileDefaults.HoverColor;
            if (State == TileStates.Unavailable)
                color = tileDefaults.UnavailableColor;
            return color;
        }
        private Color PickColor() 
        {
            Color color = tileDefaults.DefaultColor;
            switch (State) 
            {
                case TileStates.Unavailable: color = tileDefaults.DefaultColor; break;
                case TileStates.Default: color = tileDefaults.DefaultColor; break;
                case TileStates.Risky: color = tileDefaults.RiskyColor; break;
                case TileStates.Available: color = tileDefaults.AvailableColor; break;
                case TileStates.ImmenentDanger: color = tileDefaults.ImmenentDangerColor; break;
                case TileStates.Source: color = tileDefaults.SelectedColor; break;
            }
            return color;
        }

        public void SetState(TileStates state)
        {
            State = state;
            Material.color = PickColor();
        }
        public void SetIsUnderPointer(bool isTileUnderPointer)
        {
            if (!_isSelected)
            {
                Material.color = isTileUnderPointer ? PickHoverColor() : PickColor();
            }
        }

        public void SetIsSelected(bool isTileSelected)
        {
            _isSelected = isTileSelected;
            Material.color = PickColor();
            if (State != TileStates.Unavailable && isTileSelected)
                Material.color = tileDefaults.SelectedColor;
        }

        public void Configure(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
            name = $"Tile {gridPosition}";
            Material.color = tileDefaults.DefaultColor;
        }
    }
}
