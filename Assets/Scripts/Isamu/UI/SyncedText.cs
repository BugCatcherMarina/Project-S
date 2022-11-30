using TMPro;
using UnityEngine;

namespace Isamu
{
    [RequireComponent(typeof(TMP_Text))]
    public class SyncedText : SyncedComponent<TMP_Text>
    {
        protected override void ApplySettings(GameStyleSettings gameStyleSettings)
        {
            Component.font = gameStyleSettings.MenuButtonFont;
        }
    }
}
