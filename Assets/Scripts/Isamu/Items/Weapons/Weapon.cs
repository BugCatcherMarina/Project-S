using Isamu.Utils;
using UnityEngine;

namespace Isamu.Items.Weapons
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.CUSTOM_ASSET_MENU + NAME)]
    public class Weapon : ScriptableObject
    {
        private const string NAME = nameof(Weapon);
        
        public int RangeModifier => rangeModifier;
        
        [Tooltip("The amount of range added on to the base attack range of 1.")]
        [SerializeField, Min(0)] private int rangeModifier;
    }
}
