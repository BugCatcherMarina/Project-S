using System;
using System.Collections;
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
        public static event Action<UnitAsset.SpawnPosition, Action<NavigationNode>> OnStartNodeRequested; 
        
        public UnitAsset UnitAsset { get; private set; }

        [SerializeField] private GameObject unitActiveSprite;
        [SerializeField] private TMP_Text unitNameText;
        
        public NavigationNode CurrentNode { get; private set; }

        // A placeholder until we are calculating damage from sources such as abilities, statistics, weapons, buffs, etc.
        public int GetDamage()
        {
            return 1;
        }
        
        private Transform Transform
        {
            get
            {
                _transform ??= transform;
                return _transform;
            }
        }

        private Transform _transform;
        private Action _onActionComplete;

        public void Configure(UnitAsset unitAsset)
        {
            UnitAsset = unitAsset;
            unitNameText.text = unitAsset.UnitName;
            gameObject.name = unitAsset.UnitName;
            OnStartNodeRequested?.Invoke(unitAsset.Spawn, ToggleNodeBlocking);
        }

        public void MoveToTile(Tile targetTile, Action onMoveComplete)
        {
            _onActionComplete = onMoveComplete;
            OnPathRequested?.Invoke(new PathRequestInput(CurrentNode, targetTile.NavigationNode), MoveUnitAlongPath);
        }

        private void MoveUnitAlongPath(PathRequestResult path)
        {
            StartCoroutine(this.ExecuteAfterCoroutine(MoveRoutine(path), () =>
            {
                ToggleNodeBlocking(path.Nodes[0]);
                _onActionComplete?.Invoke();
            }));
        }

        private IEnumerator MoveRoutine(PathRequestResult path)
        {
            for (int i = path.NodeCount - 1; i >= 0; i--)
            {
                NavigationNode nextNode = path.Nodes[i];
                nextNode.ShowMarker(true);

                float moveTime = 0f;

                while (moveTime < UnitAsset.TileMoveDuration)
                {
                    Vector3 pos = Transform.position;
                    
                    float t = moveTime / UnitAsset.TileMoveDuration;
                    
                    float newX = Mathf.Lerp(pos.x, nextNode.X, t);
                    float newZ = Mathf.Lerp(pos.z, nextNode.Z, t);
                    
                    pos.x = newX;
                    pos.z = newZ;
                    
                    Transform.position = pos;
                    moveTime += Time.deltaTime;
                    
                    yield return null;
                }
            }
        }
        
        private void ToggleNodeBlocking(NavigationNode newNode)
        {
            if (CurrentNode != null)
            {
                CurrentNode.IsBlocked = false;
            }
                    
            CurrentNode = newNode;
            CurrentNode.IsBlocked = true;
        }

        private void Awake()
        {
            ActiveUnitHandler.OnUnitActivate += HandleUnitActivate;
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
