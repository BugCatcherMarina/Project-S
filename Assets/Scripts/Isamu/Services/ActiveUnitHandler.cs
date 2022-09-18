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

        private int unitIndex;
        private UnitBehaviour activeUnit;
        
        public ActiveUnitHandler()
        {
            UnitGenerator.OnUnitCreated += HandleUnitCreated;
            UnitGenerator.OnAllUnitsCreated += HandleAllUnitsCreated;
        }
        
        public override void Disable()
        {
            UnitGenerator.OnUnitCreated -= HandleUnitCreated;
            UnitGenerator.OnAllUnitsCreated -= HandleAllUnitsCreated;
        }

        private void HandleUnitCreated(UnitBehaviour unit)
        {
            _allUnits.Add(unit);
        }

        private void HandleAllUnitsCreated()
        {
            _allUnits = _allUnits.OrderByDescending(behaviour => behaviour.UnitAsset.Stats.Speed).ToList();

            if (_allUnits.Count > 0)
            {
                SelectUnit(0);
            }
        }

        private void SelectUnit(int index)
        {
            unitIndex = index;
            OnUnitActivate?.Invoke(_allUnits[unitIndex]);
        }
    }
}
