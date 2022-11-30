using System;
using Isamu.Utils;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Isamu
{
    [CreateAssetMenu(menuName = ProjectConsts.CUSTOM_ASSET_MENU + MENU_NAME, fileName = FILE_NAME)]
    public class GameStyleSettings : ScriptableObject
    {
        private const string MENU_NAME = "Game Style Settings";
        private const string FILE_NAME = "GameStyleSettings";

#if UNITY_EDITOR
        public static event Action<GameStyleSettings> OnSettingsModified;
#endif

        public static GameStyleSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    SetInstance();
                }

                return instance;
            }
        }

        private static GameStyleSettings instance;

        public TMP_FontAsset MenuButtonFont => menuButtonFont;

        [SerializeField] private TMP_FontAsset menuButtonFont;

        private static void SetInstance()
        {
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets("t:GameStyleSettings");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                instance = (GameStyleSettings)AssetDatabase.LoadAssetAtPath(path, typeof(GameStyleSettings));
            }
#else
            instance = Resources.Load<GameStyleSettings>(string.Empty);
#endif

            Debug.Log(instance);

            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<GameStyleSettings>();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            OnSettingsModified?.Invoke(Instance);
        }
#endif
    }

}
