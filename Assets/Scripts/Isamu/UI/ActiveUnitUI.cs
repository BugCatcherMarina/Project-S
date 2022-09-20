using Isamu.Services;
using Isamu.Units;
using TMPro;
using UnityEngine;

namespace Isamu.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ActiveUnitUI : MonoBehaviour
    {
        private TMP_Text _activeUnitText;

        private void Awake()
        {
            _activeUnitText = GetComponent<TMP_Text>();
            ActiveUnitHandler.OnUnitActivate += HandleUnitActivate;
        }

        private void OnDestroy()
        {
            ActiveUnitHandler.OnUnitActivate -= HandleUnitActivate;
        }

        private void HandleUnitActivate(UnitBehaviour unitBehaviour)
        {
            _activeUnitText.text = GetTextContent(unitBehaviour);
        }

        private static string GetTextContent(UnitBehaviour unitBehaviour)
        {
            return $"Active Unit: {unitBehaviour.UnitAsset.UnitName}";
        }
    }
}
