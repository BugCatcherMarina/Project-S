using Isamu.Map;
using Isamu.Services;
using Isamu.Utils;
using UnityEngine;

namespace Isamu.Units.TurnActions
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.ACTION_ASSET_MENU + NAME)]
    public class MoveAction : ActionAsset
    {
        private const string NAME = nameof(MoveAction);

        private UnitBehaviour _unit;
        
        public override void ExecuteAction(UnitBehaviour unitBehaviour)
        {
            _unit = unitBehaviour;
            TileSelectionHandler.OnTileSelected += HandleTileSelected;
        }

        private void HandleTileSelected(Tile tile)
        {
            TileSelectionHandler.OnTileSelected -= HandleTileSelected;
            _unit.MoveToTile(tile, HandleActionComplete);
        }
    }
}
