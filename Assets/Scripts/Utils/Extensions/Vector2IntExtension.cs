using System.Collections.Generic;
using UnityEngine;

namespace Utils.Extensions
{
    public static class Vector2IntExtension
    {
        public static IEnumerable<Vector2Int> AllInside(this Vector2Int source)
        {
            for (int x = 0; x < source.x; x++)
            for (int y = 0; y < source.y; y++)
                yield return new Vector2Int(x, y);
        }

        public static Vector3Int ToVector3Int(this Vector2Int source)
        {
            return new Vector3Int(source.x, source.y);
        }
    }
}