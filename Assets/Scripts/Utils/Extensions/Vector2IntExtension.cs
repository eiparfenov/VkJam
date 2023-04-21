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

        public static IEnumerable<Vector2Int> Rect(this Vector2Int source)
        {
            for (int i = 0; i <= source.x; i++)
            {
                yield return new Vector2Int(i, source.y);
                yield return new Vector2Int(i, -source.y);
                yield return new Vector2Int(-i, source.y);
                yield return new Vector2Int(-i, -source.y);
            }

            for (int i = 0; i < source.y; i++)
            {
                yield return new Vector2Int(source.x, i);
                yield return new Vector2Int(source.x, -i);
                yield return new Vector2Int(-source.x, i);
                yield return new Vector2Int(-source.x, -i);
            }
        }

        public static Vector3Int ToVector3Int(this Vector2Int source)
        {
            return new Vector3Int(source.x, source.y);
        }
    }
}