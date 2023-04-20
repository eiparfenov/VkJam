using UnityEngine;

namespace Utils.Extensions
{
    public static class RectTransformExtension
    {
        public static bool IsPointInside(this RectTransform rect, Vector2 point)
        {
            var point1 = (Vector2) rect.position;
            var size = rect.rect.size;
            point1 -= size / 2;
            var point2 = point1 + size;
            return point1.x < point.x && point.x < point2.x && 
                   point1.y < point.y && point.y < point2.y;
        }
    }
}