using Environment.Asteroids;
using UnityEngine;

namespace Entities.Enemies
{
    public class AStarPathFinder: PathFinder
    {
        public AStarPathFinder(MainAsteroid mainAsteroid) : base(mainAsteroid)
        {
        }

        public override Vector2Int? NextCell(Vector2Int from, Vector2Int to)
        {
            var direction = to - from;
            Vector2Int cell1, cell2;
            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                cell1 = from + Vector2Int.up * Mathf.RoundToInt(Mathf.Sign(direction.y));
                cell2 = from + Vector2Int.right * Mathf.RoundToInt(Mathf.Sign(direction.x));
            }
            else
            {
                cell1 = from + Vector2Int.up * Mathf.RoundToInt(Mathf.Sign(direction.y));
                cell2 = from + Vector2Int.right * Mathf.RoundToInt(Mathf.Sign(direction.x));
            }

            if (grid[cell1]) return cell1;
            if (grid[cell2]) return cell2;
            return null;
        }
    }
}