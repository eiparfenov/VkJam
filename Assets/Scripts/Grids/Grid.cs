using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Grids
{
    [Serializable]
    public class Grid
    {
        private HashSet<Vector2Int> _cells;

        public Grid()
        {
            _cells = new HashSet<Vector2Int>();
        }

        public Grid(IEnumerable<Vector2Int> cells)
        {
            _cells = new HashSet<Vector2Int>(cells);
        }
        
        public IEnumerable<Vector2Int> AllCells => _cells;
        public bool this[int x, int y]
        {
            get => _cells.Contains(new Vector2Int(x, y));
            set
            {
                if (value)
                    _cells.Add(new Vector2Int(x, y));
                else
                    _cells.Remove(new Vector2Int(x, y));
            }
        }
        public bool this[Vector2Int v]
        {
            get => this[v.x, v.y];
            set => this[v.x, v.y] = value;
        }

        public bool PossibleToAdd(Grid other, Vector2Int offset)
        {
            return !other._cells.Select(x => x + offset).Intersect(_cells).Any();
        }
        public int Sides(Grid other, Vector2Int offset)
        {
            return other._cells
                .SelectMany(x => new[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right }
                    .Select(dopOffset => dopOffset + offset + x))
                .Intersect(_cells)
                .Count();
        }

        public void UnionWith(Grid other, Vector2Int offset)
        {
            _cells.UnionWith(other._cells.Select(x => x + offset));
        }

        public bool PossibleToAddWithSide(Grid other, Vector2Int offset, int side)
        {
            return !other._cells
                .SelectMany(x => new[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.zero }
                    .Select(dopOffset => dopOffset * side + offset + x))
                .Intersect(_cells)
                .Any();
        }


        public static Grid FromTilemap(Tilemap tilemap)
        {
            var bounds = tilemap.cellBounds;
            var allTiles = tilemap.GetTilesBlock(bounds);
            var result = new Grid();

            for (int x = 0; x < bounds.size.x; x++) 
            {
                for (int y = 0; y < bounds.size.y; y++) 
                {
                    var tile = allTiles[x + y * bounds.size.x];
                    if (tile != null)
                    {
                        result[x, y] = true;
                    } 
                }
            }

            return result;
        }
        public static Grid FromVector2IntIEnumerable(IEnumerable<Vector2Int> source)
        {
            var result = new Grid();
            foreach (var pos in source)
            {
                result[pos] = true;
            }

            return result;
        }
        public static Grid RandomByCellsCount(int cellsCount)
        {
            var result = new Grid();
            result._cells.Add(Vector2Int.zero);
            var possibleToAdd = new HashSet<Vector2Int>()
            {
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            for (int i = 0; i < cellsCount - 1; i++)
            {
                var cellToAdd = possibleToAdd.RandomOrDefault();
                possibleToAdd.Add(cellToAdd + Vector2Int.down);
                possibleToAdd.Add(cellToAdd + Vector2Int.up);
                possibleToAdd.Add(cellToAdd + Vector2Int.left);
                possibleToAdd.Add(cellToAdd + Vector2Int.right);
                result._cells.Add(cellToAdd);
                possibleToAdd.ExceptWith(result._cells);
            }

            return result;
        }
    }
}