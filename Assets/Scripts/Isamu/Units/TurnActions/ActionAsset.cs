using System;
using UnityEngine;

namespace Isamu.Units.TurnActions
{
    public abstract class ActionAsset : ScriptableObject
    {
        public event Action<ActionAsset> OnActionComplete;
        
        public string ActionName => actionName;
        
        [SerializeField] private string actionName;
        
        public abstract void SelectAction(UnitBehaviour unitBehaviour);

        /// <summary>
        /// Called by UnitActionHandler if a new UnitActionButton is selected 
        /// </summary>
        public virtual void Cancel()
        {
            CleanUpAsset();
        }
        
        protected void HandleActionComplete()
        {
            OnActionComplete?.Invoke(this);
        }

        protected abstract void CleanUpAsset();
    }
}
