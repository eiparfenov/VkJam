using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using Signals;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class InventoryController: IDisposable
    {
        private readonly InventoryItemRowUI.Factory _rowFactory;
        private readonly SignalBus _signalBus;
        private readonly List<InventoryItemRowUI> _inventoryItemRows;
        private readonly Transform _transform;
        private readonly IInventoryStats _inventoryStats;

        public InventoryController(InventoryItemRowUI.Factory rowFactory, 
            SignalBus signalBus, IInventoryStats inventoryStats, Transform transform)
        {
            _transform = transform;
            _rowFactory = rowFactory;
            _signalBus = signalBus;
            _inventoryStats = inventoryStats;
            _inventoryItemRows = new List<InventoryItemRowUI>();
            _signalBus.Subscribe<IInventoryOpenSignal>(OnInventoryOpenClose);
        }

        private void OnInventoryOpenClose(IInventoryOpenSignal signal)
        {
            _transform.gameObject.SetActive(signal.InventoryOpened);
            if(!signal.InventoryOpened) return;
            RefreshItemRows();
        }

        private void RefreshItemRows()
        {
            foreach (var inventoryItemRowUI in _inventoryItemRows)
            {
                var inventoryItem = _inventoryStats.CollectedItems
                    .FirstOrDefault(x => x != null && x.Value.Item1 == inventoryItemRowUI.ItemData);

                inventoryItemRowUI.Count = inventoryItem?.Item2 ?? 0;
                if (inventoryItemRowUI.Count == 0)
                {
                    inventoryItemRowUI.Dispose();
                }
            }
            _inventoryItemRows.RemoveAll(x => x.Count == 0);

            foreach (var collectedItem in _inventoryStats.CollectedItems)
            {
                if(collectedItem is null) break;
                var rowItem = _inventoryItemRows
                    .FirstOrDefault(x => x.ItemData == collectedItem.Value.Item1);
                if (rowItem != null) break;
                var inventoryRowItem = _rowFactory.Create(collectedItem.Value.Item1);
                inventoryRowItem.Count = collectedItem.Value.Item2;
                _inventoryItemRows.Add(inventoryRowItem);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<IInventoryOpenSignal>(OnInventoryOpenClose);
        }
    }
}