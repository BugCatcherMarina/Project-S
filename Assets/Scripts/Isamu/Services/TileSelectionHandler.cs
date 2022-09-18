using Isamu.Map;

namespace Isamu.Services
{
    public class TileSelectionHandler : Service
    {
        private Tile selectedTile;
        
        public TileSelectionHandler()
        {
            Tile.OnClick += HandleTileClicked;
        }

        public override void Disable()
        {
            Tile.OnClick -= HandleTileClicked;
        }

        private void HandleTileClicked(Tile tile)
        {
            if (selectedTile != null)
            {
                selectedTile.SetIsSelected(false);
            }

            tile.SetIsSelected(true);
            selectedTile = tile;
        }
    }
}
