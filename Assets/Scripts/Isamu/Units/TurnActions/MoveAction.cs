using Isamu.Map;
using Isamu.Map.Navigation;
using Isamu.Services;
using Isamu.Utils;
using System;
using UnityEngine;

namespace Isamu.Units.TurnActions
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.ACTION_ASSET_MENU + NAME)]
    public class MoveAction : ActionAsset
    {
        private const int ATTACK_COST = 2;
        
        private const string NAME = nameof(MoveAction);

        public static event Action<AffordableNodesRequest, Action<AffordableNodesResult>> OnAffordableNodesRequested;
        public static event Action OnMoveSelected;
        public static event Action OnMoveComplete;
        
        private UnitBehaviour _unit;
        
        public override void SelectAction(UnitBehaviour unitBehaviour)
        {
            _unit = unitBehaviour;
            TileSelectionHandler.OnTileSelected += HandleTileSelected;

            OnMoveSelected?.Invoke();
            
            OnAffordableNodesRequested?.Invoke(new AffordableNodesRequest(_unit.CurrentNode, _unit.UnitAsset.Stats.Movement), HandleAffordableNodes);
        }

        public override void Cancel()
        {
            CleanUpAsset();
        }

        protected override void CleanUpAsset()
        {
            TileSelectionHandler.OnTileSelected -= HandleTileSelected;
        }

        private void HandleAffordableNodes(AffordableNodesResult result)
        {
            for (int i = 0, count = result.Nodes.Count; i < count; i++)
            {
                NavigationNode node = result.Nodes[i];
                int cost = result.Costs[i];
                Tile tile = node.Tile;

                if (cost + ATTACK_COST - 1 < _unit.UnitAsset.Stats.Movement)
                {
                    tile.SetState(Tile.TileStates.Available);
                }
                
                if (cost + ATTACK_COST - 1 >= _unit.UnitAsset.Stats.Movement)
                {
                    tile.SetState(Tile.TileStates.Risky);
                }
                
                if (cost == 0)
                {
                    tile.SetState(Tile.TileStates.Source);
                }
            }
        }

        private void HandleTileSelected(Tile tile)
        {
            if (tile.State is Tile.TileStates.Risky or Tile.TileStates.Available)
            {
                _unit.MoveToTile(tile, HandleActionComplete);
            }
            else 
            {
                HandleActionComplete();
            }
            
            OnMoveComplete?.Invoke();
            
            CleanUpAsset();
        }
    }
}
