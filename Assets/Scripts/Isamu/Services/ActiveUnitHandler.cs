using System;
using System.Collections.Generic;
using System.Linq;
using Isamu.Units;

namespace Isamu.Services
{
    public class ActiveUnitHandler : Service
    {
        public static event Action<UnitBehaviour> OnUnitActivate; 

        private List<UnitBehaviour> _allUnits = new();

        private int _unitIndex;
        
        public ActiveUnitHandler()
        {
            UnitGenerator.OnUnitCreated += HandleUnitCreated;
            UnitGenerator.OnAllUnitsCreated += HandleAllUnitsCreated;
            UnitActionHandler.OnUnitTurnComplete += HandleUnitTurnComplete;
        }
        
        public override void Disable()
        {
            UnitGenerator.OnUnitCreated -= HandleUnitCreated;
            UnitGenerator.OnAllUnitsCreated -= HandleAllUnitsCreated;
            UnitActionHandler.OnUnitTurnComplete -= HandleUnitTurnComplete;
        }

        private void HandleUnitCreated(UnitBehaviour unit)
        {
            _allUnits.Add(unit);
        }

        private void HandleAllUnitsCreated()
        {
            // I usually try to avoid LINQ -- but this seems like the easiest way to do this.
            // Also, we shouldn't have to run this code too frequently, so it likely won't be a performance issue.
            _allUnits = _allUnits.OrderByDescending(behaviour => behaviour.UnitAsset.Stats.Speed).ToList();

            if (_allUnits.Count > 0)
            {
                ActivateUnit(0);
            }
        }

        private void ActivateUnit(int index)
        {
            _unitIndex = index;
            OnUnitActivate?.Invoke(_allUnits[_unitIndex]);
        }

        private void HandleUnitTurnComplete()
        {
            ActivateNextUnit();
        }

        private void ActivateNextUnit()
        {
            // If we're on the last unit, go back to the beginning. Otherwise, move to the next one in the list.
            _unitIndex = _unitIndex == _allUnits.Count - 1 ? 0 : _unitIndex + 1;
            
            ActivateUnit(_unitIndex);
        }
    }
}
