using Isamu.Combat;
using Isamu.Utils;
using UnityEngine;

namespace Isamu.Units.TurnActions
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.ACTION_ASSET_MENU + NAME)]
    public class AttackAction : ActionAsset
    {
        private const string NAME = nameof(AttackAction);

        private UnitBehaviour _attacker;
        
        public override void SelectAction(UnitBehaviour unitBehaviour)
        {
            _attacker = unitBehaviour;
            Targetable.OnTargetClicked += HandleTargetClicked;
        }

        public override void Cancel()
        {
            CleanUpAsset();
        }

        protected override void CleanUpAsset()
        {
            Targetable.OnTargetClicked -= HandleTargetClicked;
            _attacker = null;
        }

        private void HandleTargetClicked(Targetable targetable)
        {
            int damage = _attacker.GetDamage();
            Debug.Log($"Unit ({_attacker.gameObject.name}) attacks target ({targetable.gameObject.name}) for {damage} damage.");
            targetable.DealDamage(damage);
            HandleActionComplete();
            CleanUpAsset();
        }
    }
}
