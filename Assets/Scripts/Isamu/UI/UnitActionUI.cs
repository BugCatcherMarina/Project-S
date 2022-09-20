using Isamu.Services;
using Isamu.Units;
using Isamu.Units.TurnActions;
using System.Collections.Generic;
using UnityEngine;

namespace Isamu.UI
{
    public class UnitActionUI : MonoBehaviour
    {
        [SerializeField] private UnitActionButton unitActionButtonPrefab;
        [SerializeField] private Transform buttonParent;

        private readonly List<UnitActionButton> _buttons = new();

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
            if (_buttons.Count > 0)
            {
                for (int i = _buttons.Count - 1; i >= 0; i--)
                {
                    Destroy(_buttons[i].gameObject);
                }
                
                _buttons.Clear();
            }
            
            CreateActionButtons(unitBehaviour);
        }

        private void CreateActionButtons(UnitBehaviour unitBehaviour)
        {
            for (int i = 0, length = unitBehaviour.UnitAsset.ActionAssets.Length; i < length; i++)
            {
                CreateButton(unitBehaviour, unitBehaviour.UnitAsset.ActionAssets[i]);
            }
        }

        private void CreateButton(UnitBehaviour unitBehaviour, ActionAsset actionAsset)
        {
            // TODO: replace Instantiate/Destroy pattern with object pooling.
            UnitActionButton button = Instantiate(unitActionButtonPrefab, buttonParent);
            button.Configure(unitBehaviour, actionAsset);
            _buttons.Add(button);
        }
    }
}
