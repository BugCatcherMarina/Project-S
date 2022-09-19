using Isamu.Units;
using Isamu.Units.TurnActions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Isamu.UI
{
    [RequireComponent(typeof(Button))]
    public class UnitActionButton : MonoBehaviour
    {
        public static event Action<UnitBehaviour, ActionAsset> OnUnitActionSelected;

        [SerializeField] private TMP_Text buttonText;
        
        private Button _button;
        private UnitBehaviour _unitBehaviour;
        private ActionAsset _actionAsset;
        
        public void Configure(UnitBehaviour unitBehaviour, ActionAsset actionAsset)
        {
            _unitBehaviour = unitBehaviour;
            _actionAsset = actionAsset;
            buttonText.text = _actionAsset.ActionName;
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleClick);   
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            OnUnitActionSelected?.Invoke(_unitBehaviour, _actionAsset);
        }
    }
}
