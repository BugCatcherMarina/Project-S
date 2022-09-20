using UnityEngine;

namespace Isamu.Utils
{
    public static class TransformExtensions
    {
        public static Vector3 CalculateTranslation(this Transform transform, Vector3 translation)
        {
            return transform.position + 
                   transform.right * translation.x + 
                   transform.up * translation.y +
                   transform.forward * translation.z;
        }
    }
}
