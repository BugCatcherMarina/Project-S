using System;
using System.Collections.Generic;
using Isamu.Map;
using Isamu.Map.Navigation;
using Isamu.Units;
using Isamu.Utils;
using UnityEngine;

namespace Isamu.Services 
{
    public class NavigationGrid : Service
    {
        private const int DIAGONAL_MULTIPLIER = 2;

        private NavigationNode[,] _grid;
        private Vector2Int _gridSize;

        public NavigationGrid()
        {
            MapGenerator.OnMapGenerated += HandleMapGenerated;
            UnitBehaviour.OnPathRequested += HandlePathRequested;
        }
        
        public override void Disable()
        {
            MapGenerator.OnMapGenerated -= HandleMapGenerated;
            UnitBehaviour.OnPathRequested -= HandlePathRequested;
        }

        private void HandleMapGenerated(MapAsset map, List<NavigationNode> nodes)
        {
            Initialize(map.Width, map.Depth, nodes);
        }

        private void HandlePathRequested(PathRequestInput input, Action<PathRequestResult> callback)
        {
            callback(new PathRequestResult(GetPath(input.From, input.To)));
        }

        private void Initialize(int width, int depth, List<NavigationNode> nodes) 
        {
            SetGridSize(width, depth);
            
            foreach (NavigationNode node in nodes)
            {
                AddNode(node);
            }

            LinkNodes();
        }

        private void SetGridSize(int width, int depth)
        {
            if (width <= 0 || depth <= 0)
            {
                Debug.LogError("Invalid Grid Size");
                return;
            }

            _gridSize = new Vector2Int(width, depth);
            _grid = new NavigationNode[width, depth];
        }

        private List<NavigationNode> GetPath(NavigationNode start, NavigationNode finish, bool goOverBlocked = false)
        {
            HideNodeMarkers();
            
            List<NavigationNode> path = new List<NavigationNode>();
            NavigationNode[,] cameFrom = new NavigationNode[_gridSize.x, _gridSize.y];
            int[,] costSoFar = new int[_gridSize.x, _gridSize.y];
            
            for (int i = 0; i < _gridSize.x; i++)
            {
                for (int j = 0; j < _gridSize.y; j++)
                {
                    costSoFar[i, j] = int.MaxValue;
                }
            }

            NavQueue frontier = new NavQueue();
            frontier.Enqueue(start, 0);
            cameFrom[start.X, start.Z] = null;
            costSoFar[start.X, start.Z] = 0;

            while (frontier.Count > 0)
            {
                NavigationNode node = frontier.Dequeue();

                if (node == finish)
                {
                    path.Add(finish);
                    NavigationNode previous = cameFrom[finish.X, finish.Z];
                    
                    while (costSoFar[previous.X, previous.Z] != 0)
                    {
                        path.Add(previous);
                        previous = cameFrom[previous.X, previous.Z];
                    }
                    
                    break;
                }

                foreach (NavigationNode neighbour in node.Links.Keys)
                {
                    int newCost = costSoFar[node.X, node.Z] + node.Links[neighbour];

                    if (newCost >= costSoFar[neighbour.X, neighbour.Z] || neighbour.IsBlocked && !goOverBlocked)
                    {
                        continue;
                    }

                    costSoFar[neighbour.X, neighbour.Z] = newCost;
                    frontier.Enqueue(neighbour, newCost);
                    cameFrom[neighbour.X, neighbour.Z] = node;
                }
            }

            return path;
        }

        private void AddNode(NavigationNode node) 
        {
            Debug.Log("Add Node");
            
            if (node == null)
            {
                Debug.LogError($"{nameof(AddNode)}: node is null");
                return;
            }

            if (!IsNodePositionValid(node.GridPosition))
            {
                Debug.LogError($"Invalid node position: {node.GridPosition}.");
                return;
            }

            if (!_grid.TrySetElementAtPosition(node.GridPosition, node))
            {
                Debug.LogError($"Node already exists at position {node.GridPosition}.");
            }
        }

        private bool IsNodePositionValid(Vector2Int position)
        {
            return position.x < _gridSize.x && position.x >= 0 && position.y < _gridSize.y && position.y >= 0;
        }

        private void LinkNodes()
        {
            for (int x = 0; x < _gridSize.x; x++) 
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    if (_grid[x, y] == null)
                    {
                        Debug.LogError("There is no node at " + new Vector2Int(x, y));
                        return;
                    }

                    if (x < _gridSize.x - 1)
                    {
                        _grid[x, y].LinkNode(_grid[x + 1, y], NavigationNode.Cost);
                    }

                    if (y < _gridSize.y - 1)
                    {
                        _grid[x, y].LinkNode(_grid[x, y + 1], NavigationNode.Cost);
                    }

                    if (y < _gridSize.y - 1 && x < _gridSize.x - 1)
                    {
                        _grid[x, y].LinkNode(_grid[x + 1, y + 1], NavigationNode.Cost * DIAGONAL_MULTIPLIER);
                    }

                    if (y > 0 && x < _gridSize.x - 1)
                    {
                        _grid[x, y].LinkNode(_grid[x + 1, y - 1], NavigationNode.Cost * DIAGONAL_MULTIPLIER);
                    }
                }
            }
        }
        
        // probably gonna change to something else later
        private void HideNodeMarkers() 
        {
            foreach (NavigationNode node in _grid) 
            {
                node.ShowMarker(false);
            }
        }
    }
}
