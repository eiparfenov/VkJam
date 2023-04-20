using System;
using Items;
using UnityEngine;

namespace Inventory
{
    public interface IInventoryDragItemProvider
    {
        public event Action<Vector2, InventoryItemUI, IInventoryDragItemProvider> onDragStarted; 
        public event Action<Vector2> onDragProcess; 
        public event Action onDragFinished;
        public ItemData ItemData { get; }
    }
}