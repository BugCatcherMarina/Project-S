using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Isamu.Map.Navigation {
    public static class NavigationGrid
    {
        const int DIAGONAL_MULTIPLIER = 2;

        public static readonly Dictionary<string, Vector2Int> Directions = new Dictionary<string, Vector2Int>()
        {
            {"north", Vector2Int.up },
            {"south" , Vector2Int.down},
            {"east", Vector2Int.right},
            {"west", Vector2Int.left },
            {"north_west", Vector2Int.left + Vector2Int.up},
            {"north_east", Vector2Int.right + Vector2Int.up },
            {"south_west", Vector2Int.left + Vector2Int.down},
            {"south_east", Vector2Int.right + Vector2Int.down }
        };

        private static NavigationNode[,] Grid;
        private static Vector2Int GridSize;

        public static void SetGridSize(Vector2Int dimensions)
        {
            NavigationNode.NavigationNodeCreated += OnNodeCreated;

            if (dimensions.x > 0 && dimensions.y > 0)
            {
                GridSize = dimensions;
                Grid = new NavigationNode[dimensions.x, dimensions.y];
                
            }
            else
            {
                Debug.LogError("Invalid Grid Size");
            }
        }

        public static void OnNodeCreated(NavigationNode node) 
        { 
            AddNode(node);
            //That's a crutch if've ever seen one. Couldn't have thought of anything better at the moment
            if(node.GridPosition == GridSize - Vector2Int.one)
                LinkNodes();
        }

        public static void AddNode(NavigationNode node) 
        {
            if (node == null)
            {
                Debug.LogError(" The node is null");
                return;
            }
            bool node_already_added = false;
            for (int x = 0; x < Grid.GetLength(0); x++) 
            {
                for (int y = 0; y < Grid.GetLength(1); y++) 
                {
                    if (node == Grid[x, y]) 
                    {
                        node_already_added = true;
                        Debug.Log(" The Node was added before");
                        return;
                    }
                }
            }
            if (!node_already_added)
            {
                if (node.GridPosition.x < GridSize.x &&
                    node.GridPosition.x >= 0 &&
                    node.GridPosition.y < GridSize.y &&
                    node.GridPosition.y >= 0)
                {
                    if (Grid[node.GridPosition.x, node.GridPosition.y] == null)
                    {
                        Grid[node.GridPosition.x, node.GridPosition.y] = node;
                    }
                    else
                    {
                        Debug.LogError("Node at position " + node.GridPosition + " already exists");
                        return;
                    }
                }
                else
                {
                    Debug.LogError("Invalid node position " + node.GridPosition);
                    return;
                }
            }
        }

        public static void LinkNodes()
        {
            for (int x = 0; x < GridSize.x; x++) 
            {
                for (int y = 0; y < GridSize.y; y++) 
                {
                    if (Grid[x, y] != null)
                    {
                       // Debug.Log(Grid[x, y]);
                        if (x < GridSize.x - 1) Grid[x, y].LinkNode(Grid[x + 1, y], Grid[x + 1, y].Cost);
                        if (y < GridSize.y - 1) Grid[x, y].LinkNode(Grid[x, y + 1], Grid[x, y + 1].Cost);
                        if (y < GridSize.y - 1 && x < GridSize.x - 1) Grid[x, y].LinkNode(Grid[x + 1, y + 1], Grid[x + 1, y + 1].Cost * DIAGONAL_MULTIPLIER);
                        if (y > 0 && x < GridSize.x - 1) Grid[x, y].LinkNode(Grid[x + 1, y - 1], Grid[x + 1, y - 1].Cost * DIAGONAL_MULTIPLIER);
                    }
                    else 
                    {
                        Debug.LogError("There is no node at " + new Vector2Int(x, y));
                        return;
                    }
                }
            }
        }
    }
    
}
