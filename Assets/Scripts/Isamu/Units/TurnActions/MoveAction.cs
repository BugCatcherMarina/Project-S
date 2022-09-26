using Isamu.Map;
using Isamu.Services;
using Isamu.Utils;
using Isamu.Map.Navigation;
using UnityEngine;

namespace Isamu.Units.TurnActions
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.ACTION_ASSET_MENU + NAME)]
    public class MoveAction : ActionAsset
    {
        private const string NAME = nameof(MoveAction);

        private UnitBehaviour _unit;
        
        // I feel like the naming here is a bit misleading.
        //Shouldn't be it something like SelectAction or PrepareAction
        public override void ExecuteAction(UnitBehaviour unitBehaviour)
        {
            _unit = unitBehaviour;
            TileSelectionHandler.OnTileSelected += HandleTileSelected;

            NavigationGrid.TilesToState(TileStates.Unavailable);
            var (nodes, costs) = NavigationGrid.GetNodesWithinCost(_unit.CurrentNode, _unit.UnitAsset.Stats.Movement);
            int attack_cost = 2;
            
            for (int i = 0; i < nodes.Count; i++)
            {
                NavigationNode node = nodes[i];
                int cost = costs[i];
                Tile _tile = node.GetComponent<Tile>();
             
                if (cost + attack_cost - 1 < _unit.UnitAsset.Stats.Movement)
                {
                    _tile.SetState(TileStates.Available);
                }
                if (cost + attack_cost - 1 >= _unit.UnitAsset.Stats.Movement)
                {
                    _tile.SetState(TileStates.Risky);
                }
                if (cost == 0)
                {
                    _tile.SetState(TileStates.Source);
                }
 
            }
        }

        private void HandleTileSelected(Tile tile)
        {
            if (tile.State == TileStates.Risky || tile.State == TileStates.Available)
            {
                TileSelectionHandler.OnTileSelected -= HandleTileSelected;
                _unit.MoveTo(tile, HandleActionComplete);
            }
            else 
            {
                HandleActionComplete();
            }
            NavigationGrid.TilesToState(TileStates.Default);
        }
    }
}
