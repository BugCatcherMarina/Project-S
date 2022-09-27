using System;
using System.Collections.Generic;
using Isamu.Map;
using Isamu.Map.Navigation;
using Isamu.Services;
using Isamu.Utils;
using UnityEngine;

namespace Isamu.Units.TurnActions
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.ACTION_ASSET_MENU + NAME)]
    public class MoveAction : ActionAsset
    {
        public class AffordableNodesRequest
        {
            public NavigationNode Start { get; }
            public int MaxCost { get; }
            public bool GoOverBlocked { get; }

            public AffordableNodesRequest(NavigationNode start, int maxCost, bool goOverBlocked = false)
            {
                Start = start;
                MaxCost = maxCost;
                GoOverBlocked = goOverBlocked;
            }
        }

        public class AffordableNodesResult
        {
            public List<NavigationNode> Nodes { get; }
            public List<int> Costs { get; }

            public AffordableNodesResult(List<NavigationNode> nodes, List<int> costs)
            {
                Nodes = nodes;
                Costs = costs;
            }
        }

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

        private void HandleAffordableNodes(AffordableNodesResult result)
        {
            for (int i = 0; i < result.Nodes.Count; i++)
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
            TileSelectionHandler.OnTileSelected -= HandleTileSelected;

            if (tile.State == Tile.TileStates.Risky || tile.State == Tile.TileStates.Available)
            {
                _unit.MoveToTile(tile, HandleActionComplete);
            }
            else 
            {
                HandleActionComplete();
            }
            
            OnMoveComplete?.Invoke();
        }
    }
}
