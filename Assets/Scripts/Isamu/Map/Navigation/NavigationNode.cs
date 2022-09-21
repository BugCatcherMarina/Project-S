using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isamu.Map;
using UnityEditor;

namespace Isamu.Map.Navigation
{
    [RequireComponent(typeof(Tile))]
    public class NavigationNode : MonoBehaviour
    {
        public static event Action<NavigationNode> NavigationNodeCreated;

        [SerializeField] private GameObject NavigationMarker;


        private bool IsBlocked = false;
        private int _cost = 1;
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public Vector2Int GridPosition
        {
            get
            {
                return GetComponent<Tile>().GridPosition;
            }
        }
        public int X
        {
            get
            {
                return GetComponent<Tile>().GridPosition.x;
            } 
        }
        public int Y
        {
            get
            {
                return GetComponent<Tile>().GridPosition.y;
            }
        }

        private Dictionary<NavigationNode, int> _links = new Dictionary<NavigationNode, int>();
        public Dictionary<NavigationNode, int> Links { get { return _links; } }

        public void ShowMarker(bool isVisible = true)
        {
            if (NavigationMarker == null) return;

            MeshRenderer renderer = NavigationMarker.GetComponent<MeshRenderer>();
            if (renderer != null) renderer.enabled = isVisible;
        }
        public void LinkNode(NavigationNode node, int cost)
        {
            if (!Links.ContainsKey(node)) 
            { 
                Links.Add(node, cost);
                if (!node.Links.ContainsKey(this)) 
                { 
                    node.Links.Add(this, cost);
                }
            }
        }
        public void UnlinkNode(NavigationNode node)
        {
            if (Links.ContainsKey(node))
            {
                Links.Remove(node);
                if (node.Links.ContainsKey(this))
                {
                    node.Links.Remove(this);
                }
            }
        }

        private void DrawDebug() {
            if (NavigationMarker == null) return;
            Vector3 pos_outbound = NavigationMarker.transform.position;
            Color color = Color.magenta;
            foreach (NavigationNode node in Links.Keys)
            {
                if (node.NavigationMarker == null) break;
                Vector3 pos_inbound = node.NavigationMarker.transform.position;
                Debug.DrawLine(pos_inbound, pos_outbound, color);
            }
        }

        private void Update()
        {
            DrawDebug();
        }


    }
}
