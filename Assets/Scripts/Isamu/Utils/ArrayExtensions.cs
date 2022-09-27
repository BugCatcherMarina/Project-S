using UnityEngine;

namespace Isamu.Utils
{
    public static class ArrayExtensions
    {
        public static bool TrySetElementAtPosition<TObject>(this TObject[,] array, Vector2Int position, TObject element)
        {
            bool doesElementExist = array[position.x, position.y] != null;

            if (!doesElementExist)
            {
                array[position.x, position.y] = element;
            }

            return !doesElementExist;
        }
    }
}
