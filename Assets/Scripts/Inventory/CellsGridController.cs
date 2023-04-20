using System.Collections.Generic;
using System.Linq;
using Installers.Inventory;
using UnityEngine;
using Zenject;
using Grid = Grids.Grid;

namespace Inventory
{
    public class CellsGridController: IInitializable
    {
        private readonly InventoryCellUI.Factory _cellFactory;
        private readonly InventoryCellUI[,] _inventoryCells;

        public CellsGridController(InventoryCellUI.Factory cellFactory,
            IInventoryControllerSettings settings)
        {
            _cellFactory = cellFactory;
            _inventoryCells = new InventoryCellUI[settings.Size.x, settings.Size.y];
        }

        public void Draw((Grid grid, Vector2Int offset, InventoryCellUI.State state)[] gridsToDraw)
        {
            Clear();
            foreach (var drawCall in gridsToDraw)
            {
                foreach (var cell in drawCall.grid.AllCells
                             .Select(v => v + drawCall.offset)
                             .Where(v => 0 <= v.x && v.x < _inventoryCells.GetLength(0) &&
                                         0 <= v.y && v.y < _inventoryCells.GetLength(1))
                             .Select(v => _inventoryCells[v.x, v.y]))
                {
                    cell.CellState = drawCall.state;
                }
            }
        }

        public void Clear()
        {
            foreach ((Vector2Int, InventoryCellUI) posAndCell in AllCells())
            {
                posAndCell.Item2.CellState = InventoryCellUI.State.Off;
            }
        }

        private IEnumerable<(Vector2Int, InventoryCellUI)> AllCells()
        {
            for (int x = 0; x < _inventoryCells.GetLength(0); x++)
            for (int y = 0; y < _inventoryCells.GetLength(1); y++)
            {
                yield return (new Vector2Int(x, y), _inventoryCells[x, y]);
            }
        }
        public void Initialize()
        {
            for (int x = 0; x < _inventoryCells.GetLength(0); x++)
            for (int y = 0; y < _inventoryCells.GetLength(1); y++)
            {
                var cell = _cellFactory.Create();
                _inventoryCells[y, _inventoryCells.GetLength(0) - x - 1] = cell;
            }
            Clear();
        }
    }
}