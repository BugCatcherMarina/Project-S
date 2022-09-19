using System;
using Isamu.Map;
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
        
        public void Configure(UnitAsset unitAsset)
        {
            UnitAsset = unitAsset;
            unitNameText.text = unitAsset.UnitName;
        }
        
        // Obviously this should be replaced with pathfinding eventually -- this is just temporary.
        // Teleportation! lol. This also doesn't take into account a unit's Movement stat yet.
        public void MoveTo(Tile tile, Action onMoveComplete)
        {
            Vector3 position = Transform.position;
            position.x = tile.X;
            position.z = tile.Z;
            Transform.position = position;
            onMoveComplete?.Invoke();
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
