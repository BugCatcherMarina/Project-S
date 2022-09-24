using Isamu.Map;
using Isamu.Services;
using TMPro;
using UnityEngine;

namespace Isamu.UI.tmp
{
    public class Status : MonoBehaviour
    {
        private const string NULL_TILE_TEXT = "NULL";

        private TMP_Text _statusText;
        
        private void Start()
        {
            TileSelectionHandler.OnTileSelected += OnTileSelected;
            TileSelectionHandler.OnTileHoverBegin += OnTileHoverBegin;
            TileSelectionHandler.OnTileHoverEnd += OnTileHoverEnd;
            
            // Caching this once in Start for re-use.
            _statusText = GetComponent<TMP_Text>();
        }

        private void OnDestroy()
        {
            // Unsubscribing from these events to avoid memory leaks. 
            TileSelectionHandler.OnTileSelected -= OnTileSelected;
            TileSelectionHandler.OnTileHoverBegin -= OnTileHoverBegin;
            TileSelectionHandler.OnTileHoverEnd -= OnTileHoverEnd;
        }

        private void OnTileSelected(Tile tile) 
        {
            UpdateText(tile, TileSelectionHandler.HoveringTile);
        }

        private void OnTileHoverBegin(Tile tile)
        {
            UpdateText(TileSelectionHandler.SelectedTile, tile);
        }

        private void OnTileHoverEnd()
        {
            UpdateText(TileSelectionHandler.SelectedTile);
        }

        private void UpdateText(Tile selectedTile = null, Tile hoveringTile = null)
        {
            string selectedTileText = NULL_TILE_TEXT;
            string hoveringTileText = NULL_TILE_TEXT;
            
            if (selectedTile != null)
            {
                selectedTileText = selectedTile.transform.name;
            }

            if (hoveringTile != null)
            {
                hoveringTileText = hoveringTile.transform.name;
            }

            _statusText.text = selectedTileText + " is selected. Hovering over " + hoveringTileText;
        }
    }
}
