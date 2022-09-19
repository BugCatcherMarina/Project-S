using System;
using Isamu.Map;

namespace Isamu.Services
{
    public class TileSelectionHandler : Service
    {
        // By having separate events from Tile's events, we have the ability in the future to perform certain logic in
        // TileSelectionHandler to determine if a tile CAN be selected or hovered. TileSelectionHandler can listen
        // for the raw input events from Tile, then invoke these events that other components can listen to.
        public static event Action<Tile> OnTileSelected; 
        public static event Action<Tile> OnTileHoverBegin; 
        public static event Action OnTileHoverEnd; 

        // There should only ever be one selected tile or hovering tile (for now) so these can be static.
        // Their getter is public so they can be accessed from anywhere but their setting is private,
        // so they can only be set from this script.
        public static Tile SelectedTile { get; private set; }
        public static Tile HoveringTile { get; private set; }

        public TileSelectionHandler()
        {
            Tile.OnClick += HandleTileClicked;
            Tile.OnPointerEnter += HandleTilePointerEnter;
            Tile.OnPointerExit += HandleTilePointerExit;
        }

        public override void Disable()
        {
            Tile.OnClick -= HandleTileClicked;
            Tile.OnPointerEnter -= HandleTilePointerEnter;
            Tile.OnPointerExit -= HandleTilePointerExit;
        }

        private static void HandleTileClicked(Tile tile)
        {
            if (SelectedTile != null)
            {
                SelectedTile.SetIsSelected(false);
            }

            tile.SetIsSelected(true);
            SelectedTile = tile;
            OnTileSelected?.Invoke(SelectedTile);
        }
        
        private static void HandleTilePointerEnter(Tile tile) 
        {
            HoveringTile = tile;
            tile.SetIsUnderPointer(true);
            OnTileHoverBegin?.Invoke(HoveringTile);
        }
        
        private void HandleTilePointerExit(Tile tile)
        {
            HoveringTile = null;
            tile.SetIsUnderPointer(false);
            OnTileHoverEnd?.Invoke();
        }
    }
}
