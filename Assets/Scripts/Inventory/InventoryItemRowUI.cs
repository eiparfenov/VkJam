using System;
using System.Linq;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Inventory
{
    public class InventoryItemRowUI: IDisposable, IInventoryDragItemProvider
    {
        public class Factory : PlaceholderFactory<ItemData, InventoryItemRowUI> { }
        private readonly DragAbleUI _dragAbleUI;
        private readonly ItemData _itemData;
        private readonly Image _image;
        private readonly TextMeshProUGUI _description;
        private readonly TextMeshProUGUI _count;
        private readonly TextMeshProUGUI _name;
        private readonly InventoryItemUI.Factory _inventoryItemUiFactory;
        public int Count
        {
            get => int.Parse(_count.text);
            set => _count.text = value.ToString();
        }

        public InventoryItemRowUI(DragAbleUI dragAbleUI, ItemData itemData, Image[] images, 
            TextMeshProUGUI[] texts, InventoryItemUI.Factory inventoryItemUiFactory)
        {
            _dragAbleUI = dragAbleUI;
            _itemData = itemData;
            _image = images.First(x => x.name == "Icon");
            _description = texts.First(x => x.name == "Description");
            _count = texts.First(x => x.name == "Count");
            _name = texts.First(x => x.name == "Name");
            _inventoryItemUiFactory = inventoryItemUiFactory;
            
            _dragAbleUI.onDragStarted += DragAbleUIOnDragStarted;
            _dragAbleUI.onDragProgress += DragAbleUIOnDragProgress;
            _dragAbleUI.onDragFinished += DragAbleUIOnDragFinished;
            
            DisplayItem();
        }

        public ItemData ItemData => _itemData;
        public event Action<Vector2, InventoryItemUI, IInventoryDragItemProvider> onDragStarted; 
        public event Action<Vector2> onDragProcess; 
        public event Action onDragFinished;

        private void DisplayItem()
        {
            _image.sprite = _itemData.Icon;
            _description.text = _itemData.Description;
            _name.text = _itemData.Name;
            Count = 0;
        }

        private void DragAbleUIOnDragFinished(Vector2 obj)
        {
            onDragFinished?.Invoke();
        }

        private void DragAbleUIOnDragProgress(Vector2 obj)
        {
            onDragProcess?.Invoke(obj);
        }

        private void DragAbleUIOnDragStarted(Vector2 obj)
        {
            var item = _inventoryItemUiFactory.Create(_itemData);
            onDragStarted?.Invoke(obj, item, this);
        }
        
        public void Dispose()
        {
            Object.Destroy(_dragAbleUI.transform.parent.gameObject);
             _dragAbleUI.onDragStarted -= DragAbleUIOnDragStarted;
            _dragAbleUI.onDragProgress -= DragAbleUIOnDragProgress;
            _dragAbleUI.onDragFinished -= DragAbleUIOnDragFinished;
        }
    }
}