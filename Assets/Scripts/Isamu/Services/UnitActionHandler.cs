using System;
using Isamu.UI;
using Isamu.Units;
using Isamu.Units.TurnActions;

namespace Isamu.Services
{
    public class UnitActionHandler : Service
    {
        public static event Action OnUnitTurnComplete;

        private ActionAsset _selectedAction;
        
        public UnitActionHandler()
        {
            UnitActionButton.OnUnitActionSelected += HandleActionSelected;
        }
        
        public override void Disable()
        {
            UnitActionButton.OnUnitActionSelected += HandleActionSelected;
        }

        private void HandleActionSelected(UnitBehaviour unitBehaviour, ActionAsset actionAsset)
        {
            // If an action was already queued, cancel it before selecting the new action.
            if (_selectedAction != null)
            {
                _selectedAction.Cancel();
            }

            _selectedAction = actionAsset;
            actionAsset.OnActionComplete += HandleActionComplete;
            actionAsset.SelectAction(unitBehaviour);
        }

        private void HandleActionComplete(ActionAsset actionAsset)
        {
            actionAsset.OnActionComplete -= HandleActionComplete;
            OnUnitTurnComplete?.Invoke();
            _selectedAction = null;
        }
    }
}
