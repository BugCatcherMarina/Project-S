using System;
using UnityEngine;

namespace Isamu.Combat
{
    public class Targetable : MonoBehaviour
    {
        public static event Action<Targetable> OnTargetClicked;

        // Called from InteractableBehaviour IPointerClickHandler events.
        public void InvokeTargetClicked()
        {
            OnTargetClicked?.Invoke(this);
        }

        public void DealDamage(int damage)
        {
            // APPLY DAMAGE TO HEALTH HERE.
            // We could raise a UnityEvent<int> in this class. Or perhaps cache the health component in this class.
        }
    }
}
