using UnityEngine;

namespace Utils.Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 RoundToInt(this Vector2 source)
        {
            return new Vector2(Mathf.RoundToInt(source.x), Mathf.RoundToInt(source.y));
        }

        public static Vector2Int RoundToVector2Int(this Vector2 source)
        {
            return new Vector2Int(Mathf.RoundToInt(source.x), Mathf.RoundToInt(source.y));
        }
    }
}