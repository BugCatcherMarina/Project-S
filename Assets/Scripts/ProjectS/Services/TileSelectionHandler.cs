using ProjectS.Map;

namespace ProjectS.Services
{
    public class TileSelectionHandler : Service
    {
        private Tile selectedTile;
        private Tile tileUnderPointer;


        public TileSelectionHandler()
        {
            Tile.OnClick += HandleTileClicked;
            Tile.OnPointerEnter += HandleTilePointerEnter;
            Tile.OnPointerExit += HandleTilePointerExit;
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
        private void HandleTilePointerEnter(Tile tile) {
            tileUnderPointer = tile;
            tile.SetIsUnderPointer(true);
        }
        private void HandleTilePointerExit(Tile tile){
            tileUnderPointer = null;
            tile.SetIsUnderPointer(false);
        }
    }
}
