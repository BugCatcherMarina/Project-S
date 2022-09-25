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
        public static event Action<PathRequestInput, Action<PathRequestResult>> OnPathRequested;
        
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
        private Action _onActionComplete;
        
        private void UpdateCurrentNode() 
        {
            Vector3 start = transform.position;
            Vector3 direction = Vector3.down;
            Physics.Raycast(start, direction, out var hit);

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

        public void MoveToTile(Tile targetTile, Action onMoveComplete)
        {
            _onActionComplete = onMoveComplete;
            OnPathRequested?.Invoke(new PathRequestInput(_currentNode, targetTile.NavigationNode), MoveUnitAlongPath);
        }

        private void MoveUnitAlongPath(PathRequestResult path)
        {
            const float moveDuration = 1f;

            for (int i = 0; i < path.NodeCount; i++)
            {
                NavigationNode nextNode = path.Nodes[i];
                nextNode.ShowMarker(true);

                float moveTime = 0f;

                while (moveTime < moveDuration)
                {
                    Vector3 pos = Transform.position;
                    float t = moveTime / moveDuration;
                    float newX = Mathf.Lerp(pos.x, nextNode.X, t);
                    float newZ = Mathf.Lerp(pos.z, nextNode.Z, t);
                    pos.x = newX;
                    pos.z = newZ;
                    Transform.position = pos;
                    
                    moveTime += Time.deltaTime;
                }

                if (i != path.NodeCount - 1)
                {
                    continue;
                }

                if (_currentNode != null)
                {
                    _currentNode.IsBlocked = false;
                }
                    
                _currentNode = nextNode;
                _currentNode.IsBlocked = true;
            }
            
            _onActionComplete?.Invoke();
        }

        private void LeaveCurrentNode() 
        {
            Debug.Assert(_currentNode != null, "Unit is not attached to a navigation node");
            _currentNode.IsBlocked = false;
        }

        private void EnterNode()
        {
            UpdateCurrentNode();
            
            if (_currentNode != null)
            {
                _currentNode.IsBlocked = true;
            }
        }

        private void Awake()
        {
            ActiveUnitHandler.OnUnitActivate += HandleUnitActivate;
        }

        private void Start()
        {
            EnterNode();
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
