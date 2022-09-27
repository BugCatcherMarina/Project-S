using System;
using System.Collections.Generic;
using Isamu.Map;
using Isamu.Map.Navigation;
using Isamu.Services;
using TMPro;
using UnityEngine;

namespace Isamu.Units
{
    public class UnitBehaviour : MonoBehaviour
    {

        public UnitAsset UnitAsset { get; private set; }

        [SerializeField] private GameObject unitActiveSprite;
        [SerializeField] private TMP_Text unitNameText;
        
        private Transform Transform
        {
            get
            {
                _transform ??= transform;
                return _transform;
            }
        }

        private Transform _transform;

        private NavigationNode _currentNode;
        public NavigationNode CurrentNode { get { return _currentNode; } }

        private void UpdateCurrentNode() 
        {

            Vector3 start = transform.position;
            Vector3 direction = Vector3.down;
            RaycastHit hit;
            Physics.Raycast(start, direction, out hit);

            NavigationNode node = hit.collider.gameObject.GetComponent<NavigationNode>();
            if (node != null)
            {
                _currentNode = node;
            }
            else
            {
                _currentNode = null;
                Debug.Log("That's odd there is no Navigation Node Under Me");
            }

        }

        public void Configure(UnitAsset unitAsset)
        {
            UnitAsset = unitAsset;
            unitNameText.text = unitAsset.UnitName;
        }
        
        // Obviously this should be replaced with pathfinding eventually -- this is just temporary.
        // Teleportation! lol. This also doesn't take into account a unit's Movement stat yet.
        public void MoveTo(Tile tile, Action onMoveComplete)
        {
            LeaveCurrentNode();

            NavigationGrid.HideNodeMarkers();


            Vector3 position = Transform.position;
            position.x = tile.X;
            position.z = tile.Z;
            Vector2Int finish = new Vector2Int((int)position.x, (int)position.z);

            List<NavigationNode> path =  NavigationGrid.GetPath(_currentNode, tile.NavigationNode);
            foreach (NavigationNode node in path)
            {
                node.ShowMarker(true);
            }

            Transform.position = position;

            EnterNode();


            onMoveComplete?.Invoke();
        }


        public void LeaveCurrentNode() 
        {
            Debug.Assert(_currentNode != null, "Unit is not attached to a navigation node");
            _currentNode.IsBlocked = false;
        }
        public void EnterNode()
        {
            UpdateCurrentNode();
            if (_currentNode != null) _currentNode.IsBlocked = true;
        }
        private void Awake()
        {
            ActiveUnitHandler.OnUnitActivate += HandleUnitActivate;
        }
        private void Start()
        {
            EnterNode();
            Debug.Log(_currentNode.GridPosition);
        }
        private void OnDestroy()
        {
            ActiveUnitHandler.OnUnitActivate -= HandleUnitActivate;
        }

        private void HandleUnitActivate(UnitBehaviour unitBehaviour)
        {
            unitActiveSprite.SetActive(unitBehaviour == this);

           
        }
    }
}
