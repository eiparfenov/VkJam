using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Installers.Inventory;
using Shared;
using Signals;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Inventory
{
    public class InventoryController: IDisposable, IInitializable
    {
        private readonly InventoryItemRowUI.Factory _rowFactory;
        private readonly SignalBus _signalBus;
        private readonly Transform _transform;
        private readonly IInventoryStats _inventoryStats;
        private readonly RectTransform _cellsGrid;
        private readonly IInventoryItemUISettings _cellUiSettings;
        private readonly CellsGridController _cellsGridController;
        private readonly Transform _dragContainer;
        private readonly Transform _placedContainer;
        private readonly InventoryProcessor _inventoryProcessor;

        private readonly List<InventoryItemRowUI> _inventoryItemRows;
        private readonly List<InventoryItemUI> _dragItems;
        public InventoryController(
            InventoryItemRowUI.Factory rowFactory,
            SignalBus signalBus, 
            IInventoryStats inventoryStats, 
            Transform transform,
            [Inject(Id = "CellsGrid")]  RectTransform cellsGrid, 
            IInventoryItemUISettings cellUiSettings,
            CellsGridController cellsGridController, 
            [Inject(Id = "DragContainer")] Transform dragContainer, 
            [Inject(Id = "PlaceContainer")] Transform placedContainer,
            InventoryProcessor inventoryProcessor)
        {
            _inventoryProcessor = inventoryProcessor;
            _placedContainer = placedContainer;
            _dragContainer = dragContainer;
            _cellsGrid = cellsGrid;
            _cellsGridController = cellsGridController;
            _dragItems = new List<InventoryItemUI>();
            _transform = transform;
            _rowFactory = rowFactory;
            _signalBus = signalBus;
            _inventoryStats = inventoryStats;
            _cellUiSettings = cellUiSettings;
            _inventoryItemRows = new List<InventoryItemRowUI>();
            _signalBus.Subscribe<IInventoryOpenSignal>(OnInventoryOpenClose);
        }
        public void Initialize()
        {
            
        }
        private void OnInventoryOpenClose(IInventoryOpenSignal signal)
        {
            _transform.gameObject.SetActive(signal.InventoryOpened);
            if(!signal.InventoryOpened) return;
            RefreshItemRows();
            
        }
        private void RefreshItemRows(bool destroyZero = true)
        {
            foreach (var inventoryItemRowUI in _inventoryItemRows)
            {
                var inventoryItem = _inventoryStats.CollectedItems
                    .FirstOrDefault(x => x != null && x.ItemData == inventoryItemRowUI.ItemData);

                inventoryItemRowUI.Count = inventoryItem?.Count?? 0;
                if (inventoryItemRowUI.Count == 0 && destroyZero)
                {
                    inventoryItemRowUI.Dispose();
                }
            }

            foreach (var itemRowUI in _inventoryItemRows.Where(x => x.Count == 0))
            {
                itemRowUI.onDragStarted -= ItemDragHandler;
                itemRowUI.Dispose();
            }
            _inventoryItemRows.RemoveAll(x => x.Count == 0);

            foreach (var collectedItem in _inventoryStats.CollectedItems)
            {
                if(collectedItem is null) break;
                var rowItem = _inventoryItemRows
                    .FirstOrDefault(x => x.ItemData == collectedItem.ItemData);
                if (rowItem != null) break;
                var inventoryRowItem = _rowFactory.Create(collectedItem.ItemData);
                inventoryRowItem.Count = collectedItem.Count;
                inventoryRowItem.onDragStarted += ItemDragHandler;
                _inventoryItemRows.Add(inventoryRowItem);
            }
        }

        private async void ItemDragHandler(Vector2 pos, InventoryItemUI item, IInventoryDragItemProvider dragSource)
        {
            item.SetParent(_dragContainer);
            if (item.Offset.HasValue)
            {
                _inventoryProcessor.UnPin(item.Offset.Value);
            }
            else
            {
                _inventoryStats.RemoveItem(item.ItemData);
                RefreshItemRows(false);
            }
            
            var grid = new Grids.Grid(dragSource.ItemData.Cells);
            var dragging = true;
            var onMove = new Action<Vector2>(newPos =>
            {
                pos = newPos;
                item.Position = pos;
                var posOnGrid = PosOnGrid(pos);
                var possibleToPlace = _inventoryProcessor.PossibleToPlace(grid, posOnGrid);
                _cellsGridController.Draw(new [] { (grid, posOnGrid, possibleToPlace? InventoryCellUI.State.CanPlace: InventoryCellUI.State.CantPlace) });
                //_inventoryCells[posOnGrid.x, posOnGrid.y].CellState = InventoryCellUI.State.CanPlace;
            });
            var onStop = new Action(() => dragging = false);

            dragSource.onDragProcess += onMove;
            dragSource.onDragFinished += onStop;

            await UniTask.WaitWhile(() => dragging);
            
            dragSource.onDragProcess -= onMove;
            dragSource.onDragFinished -= onStop;
            if (!_cellsGrid.IsPointInside(pos))
            {
                _inventoryStats.AddItem(item.ItemData);
                item.onDragStarted -= ItemDragHandler;
                item.Dispose();
            }
            else
            {
                var posOnGrid = PosOnGrid(pos);
                var possibleToPlace = _inventoryProcessor.PossibleToPlace(grid, posOnGrid);
                Vector2Int? positionToPlace = possibleToPlace ? new Vector2Int?(posOnGrid) : item.Offset;
                if (!positionToPlace.HasValue)
                {
                    _inventoryStats.AddItem(item.ItemData);
                    item.onDragStarted -= ItemDragHandler;
                    item.Dispose();
                }
                else
                {
                    item.Position = GridToPos(positionToPlace.Value);
                    _inventoryProcessor.Pin(positionToPlace.Value, item.ItemData);
                    item.Offset = positionToPlace;
                    item.SetParent(_placedContainer);
                    if(item != dragSource)
                    {
                        _dragItems.Add(item);
                        item.onDragStarted += ItemDragHandler;
                    }
                }
            }
        }
        private Vector2Int PosOnGrid(Vector2 pos) => 
            (
                (pos - 
                    (Vector2)_cellsGrid.position + 
                    _cellsGrid.rect.size / 2 - 
                    _cellUiSettings.CellSize / 2
                    ) /
                _cellUiSettings.CellSize).RoundToVector2Int();
        private Vector2 GridToPos(Vector2Int posOnGrid) =>
            (posOnGrid + Vector2.one / 2) * _cellUiSettings.CellSize + 
            (Vector2)_cellsGrid.position - 
            (Vector2)_cellsGrid.rect.size / 2;
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<IInventoryOpenSignal>(OnInventoryOpenClose);
        }
        
    }
}