using UnityEngine;

namespace Entities.Enemies
{
    public interface IPathFinder
    {
        Vector2Int? NextCell(Vector2Int from, Vector2Int to);
    }
}