using System;
using UnityEngine;
using Isamu.Map.Navigation;
using Isamu.Units.TurnActions;

namespace Isamu.Map
{
    public class Tile : MonoBehaviour
    {
        public enum TileStates
        {
            Default,
            Available,
            Unavailable,
            Risky,
            ImminentDanger,
            Source
        }
        
        public static event Action<Tile> OnClick;
        public static event Action<Tile> OnPointerEnter;
        public static event Action<Tile> OnPointerExit;

        [SerializeField] private TileDefaults tileDefaults;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private bool _isSelected;
        
        public Vector2Int GridPosition { get; private set; }

        public NavigationNode NavigationNode => navigationNode;
        [SerializeField] private NavigationNode navigationNode;

        public TileStates State { get; set; } = TileStates.Default;

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
                Material.color = isTileUnderPointer ? PickHoverColor() : PickColor();
            }
        }

        public void SetIsSelected(bool isTileSelected)
        {
            _isSelected = isTileSelected;
            Material.color = PickColor();
            
            if (State != TileStates.Unavailable && isTileSelected)
            {
                Material.color = tileDefaults.SelectedColor;
            }
        }

        public void Configure(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
            name = $"Tile {gridPosition}";
            Material.color = tileDefaults.DefaultColor;
        }
        
        public void SetState(TileStates state)
        {
            State = state;
            Material.color = PickColor();
        }
        
        private Color PickHoverColor()
        {
            Color color = tileDefaults.HoverColor;
            
            if (State == TileStates.Unavailable)
            {
                color = tileDefaults.UnavailableColor;
            }

            return color;
        }
        
        private Color PickColor()
        {
            Color color = State switch
            {
                TileStates.Unavailable => tileDefaults.DefaultColor,
                TileStates.Default => tileDefaults.DefaultColor,
                TileStates.Risky => tileDefaults.RiskyColor,
                TileStates.Available => tileDefaults.AvailableColor,
                TileStates.ImminentDanger => tileDefaults.ImminentDangerColor,
                TileStates.Source => tileDefaults.SelectedColor,
                _ => tileDefaults.DefaultColor
            };
            return color;
        }

        private void Awake()
        {
            MoveAction.OnMoveSelected += HandleMoveSelected;
            MoveAction.OnMoveComplete += HandleMoveComplete;
        }

        private void OnDestroy()
        {
            MoveAction.OnMoveSelected -= HandleMoveSelected;
            MoveAction.OnMoveComplete -= HandleMoveComplete;
        }

        private void HandleMoveSelected()
        {
            SetState(TileStates.Unavailable);
        }

        private void HandleMoveComplete()
        {
            SetState(TileStates.Default);
        }
    }
}
