using Environment.Asteroids;
using UnityEngine;
using Grid = Grids.Grid;


namespace Entities.Enemies
{
    public abstract class PathFinder: IPathFinder
    {
        protected readonly Grid grid;

        protected PathFinder(MainAsteroid mainAsteroid)
        {
            grid = mainAsteroid.MainGrid;
        }

        public abstract Vector2Int? NextCell(Vector2Int from, Vector2Int to);
    }
}