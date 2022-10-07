using System;
using UnityEngine;

namespace Isamu.Combat
{
    public class Targetable : MonoBehaviour
    {
        public static event Action<Targetable> OnTargetClicked;

        public void InvokeTargetClicked()
        {
            OnTargetClicked?.Invoke(this);
        }

        public void DealDamage(int damage)
        {
            // APPLY DAMAGE TO HEALTH HERE.
        }
    }
}
