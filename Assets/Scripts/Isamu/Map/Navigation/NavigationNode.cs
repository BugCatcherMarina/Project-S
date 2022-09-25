using System.Collections.Generic;
using UnityEngine;

namespace Isamu.Map.Navigation
{
    [RequireComponent(typeof(Tile))]
    public class NavigationNode : MonoBehaviour
    {
        private const float DEBUG_SPHERE_RADIUS = 0.4f;
        private readonly Color _gizmosBlockedColor = Color.red;
        private readonly Color _gizmosDefaultColor = Color.green;

        public bool IsBlocked
        {
            get => isBlocked;
            set => isBlocked = value;
        }

        public static int Cost => 1;

        public Vector2Int GridPosition => _tile.GridPosition;
        public int X => _tile.GridPosition.x;
        public int Z => _tile.GridPosition.y;
        public Dictionary<NavigationNode, int> Links { get; } = new();

        [SerializeField] private GameObject navigationMarker;
        [SerializeField] private bool isBlocked;
        
        private Tile _tile;
        private MeshRenderer _navMarkerRend;

        public void ShowMarker(bool isVisible)
        {
            if (navigationMarker != null)
            {
                _navMarkerRend.enabled = isVisible;
            }
        }

        public void LinkNode(NavigationNode node, int cost)
        {
            if (Links.ContainsKey(node))
            {
                return;
            }

            Links.Add(node, cost);
            
            if (!node.Links.ContainsKey(this)) 
            { 
                node.Links.Add(this, cost);
            }
        }

        public void UnlinkNode(NavigationNode node)
        {
            if (!Links.ContainsKey(node))
            {
                return;
            }

            Links.Remove(node);

            if (node.Links.ContainsKey(this))
            {
                node.Links.Remove(this);
            }
        }

        private void DrawDebug() 
        {
            if (navigationMarker == null)
            {
                return;
            }

            Vector3 posOutbound = navigationMarker.transform.position;
            Color color = Color.magenta;
            
            foreach (NavigationNode node in Links.Keys)
            {
                if (node.navigationMarker == null)
                {
                    break;
                }

                Vector3 posInbound = node.navigationMarker.transform.position;
                Debug.DrawLine(posInbound, posOutbound, color);
            }
        }

        private void OnDrawGizmos()
        {
            if (navigationMarker == null)
            {
                return;
            }

            Vector3 position = navigationMarker.transform.position;

            Gizmos.color = IsBlocked ? _gizmosBlockedColor : _gizmosDefaultColor;
            Gizmos.DrawSphere(position, DEBUG_SPHERE_RADIUS);
        }

        #if UNITY_EDITOR
        private void Update()
        {
            DrawDebug();
        }
        #endif

        private void Awake()
        {
            _tile = GetComponent<Tile>();
            _navMarkerRend = navigationMarker.GetComponent<MeshRenderer>();
        }
    }
}
