using System;
using Isamu.UI;
using Isamu.Units;
using Isamu.Units.TurnActions;

namespace Isamu.Services
{
    public class UnitActionHandler : Service
    {
        public static event Action OnUnitTurnComplete;
        
        public UnitActionHandler()
        {
            UnitActionButton.OnUnitActionSelected += HandleActionSelected;
        }
        
        public override void Disable()
        {
            UnitActionButton.OnUnitActionSelected += HandleActionSelected;
        }

        private static void HandleActionSelected(UnitBehaviour unitBehaviour, ActionAsset actionAsset)
        {
            actionAsset.OnActionComplete += HandleActionComplete;
            actionAsset.SelectAction(unitBehaviour);
        }

        private static void HandleActionComplete(ActionAsset actionAsset)
        {
            actionAsset.OnActionComplete -= HandleActionComplete;
            OnUnitTurnComplete?.Invoke();
        }
    }
}
