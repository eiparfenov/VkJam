using System;
using Installers.Inventory;
using Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Inventory
{
    public class InventoryItemUI: IInventoryDragItemProvider, IDisposable
    {
        public class Factory: PlaceholderFactory<ItemData, InventoryItemUI> { }
        private readonly ItemData _itemData;
        private readonly Image _icon;
        private readonly RectTransform _transform;
        private readonly DragAbleUI _dragAble;
        public Vector2 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }
        public void SetParent(Transform parent)
        {
            _transform.SetParent(parent, true);
        }

        public Vector2Int? Offset { get; set; }

        public InventoryItemUI(ItemData itemData, Image icon, RectTransform transform, IInventoryItemUISettings settings, DragAbleUI dragAble)
        {
            _itemData = itemData;
            _icon = icon;
            _transform = transform;
            _dragAble = dragAble;
            
            _transform.sizeDelta = settings.CellSize;
            _icon.sprite = itemData.Icon;
            
            dragAble.onDragStarted += DragAbleOnDragStarted;
            dragAble.onDragProgress += DragAbleOnDragProgress;
            dragAble.onDragFinished += DragAbleOnDragFinished;
        }

        private void DragAbleOnDragFinished(Vector2 obj)
        {
            onDragFinished?.Invoke();
        }

        public ItemData ItemData => _itemData;

        private void DragAbleOnDragProgress(Vector2 obj)
        {
            onDragProcess?.Invoke(obj);
        }

        private void DragAbleOnDragStarted(Vector2 obj)
        {
            onDragStarted?.Invoke(obj, this, this);
        }

        public event Action<Vector2, InventoryItemUI, IInventoryDragItemProvider> onDragStarted;
        public event Action<Vector2> onDragProcess;
        public event Action onDragFinished;

        public void Dispose()
        {
            _dragAble.onDragStarted += DragAbleOnDragStarted;
            _dragAble.onDragProgress += DragAbleOnDragProgress;
            _dragAble.onDragFinished += DragAbleOnDragFinished;
            
            Object.Destroy(_icon.gameObject);
        }
    }
}