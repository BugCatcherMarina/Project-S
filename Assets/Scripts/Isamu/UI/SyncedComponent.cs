using System.Diagnostics;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Isamu
{
    public abstract class SyncedComponent<TComponent> : MonoBehaviour where TComponent : MonoBehaviour
    {
        protected abstract void ApplySettings(GameStyleSettings gameStyleSettings);

        private TComponent component;

        protected TComponent Component
        {
            get
            {
                if (component == null)
                {
                    component = GetComponent<TComponent>();
                }

                Debug.Assert(component != null, $"{typeof(TComponent)} is missing from {gameObject.name}.");
                return component;
            }
        }

    #if UNITY_EDITOR
        private void OnValidate()
        {
            GameStyleSettings.OnSettingsModified -= ApplySettings;
            GameStyleSettings.OnSettingsModified += ApplySettings;
            TryApplySettings();
        }
        #endif

        private void Start()
        {
            TryApplySettings();
        }

        private void TryApplySettings()
        {
            UnityEngine.Debug.Log("TryApplySettings");
            GameStyleSettings settings = GameStyleSettings.Instance;

            if (settings != null)
            {
                ApplySettings(settings);
            }
        }
    }
}
