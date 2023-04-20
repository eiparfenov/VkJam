using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
using Grid = Grids.Grid;

namespace Inventory
{
    public class InventoryProcessor
    {
        private readonly List<(ItemData itemData, Grid grid, Vector2Int offset)> _installedItems;

        public InventoryProcessor()
        {
            _installedItems = new();
        }

        public void UnPin(Vector2Int offset)
        {
            _installedItems.RemoveAll(x => x.offset == offset);
        }

        public void Pin(Vector2Int offset, ItemData itemData)
        {
            _installedItems.Add((itemData, new Grid(itemData.Cells), offset));
        }

        public bool PossibleToPlace(Grid toPlace, Vector2Int offset)
        {
            return _installedItems.All(installedItem => installedItem.grid.PossibleToAdd(toPlace, offset - installedItem.offset));
        }
    }
}