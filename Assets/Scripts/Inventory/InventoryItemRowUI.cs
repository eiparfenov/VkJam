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
    public class InventoryItemRowUI: IDisposable
    {
        public class Factory : PlaceholderFactory<ItemData, InventoryItemRowUI> { }
        private readonly DragAbleUI _dragAbleUI;
        private readonly ItemData _itemData;
        private readonly Image _image;
        private readonly TextMeshProUGUI _description;
        private readonly TextMeshProUGUI _count;
        private readonly TextMeshProUGUI _name;
        public int Count
        {
            get => int.Parse(_count.text);
            set => _count.text = value.ToString();
        }

        public InventoryItemRowUI(DragAbleUI dragAbleUI, ItemData itemData, Image[] images, 
            TextMeshProUGUI[] texts)
        {
            _dragAbleUI = dragAbleUI;
            _itemData = itemData;
            _image = images.First(x => x.name == "Icon");
            _description = texts.First(x => x.name == "Description");
            _count = texts.First(x => x.name == "Count");
            _name = texts.First(x => x.name == "Name");
            
            _dragAbleUI.onDragStarted += DragAbleUIOnDragStarted;
            _dragAbleUI.onDragProgress += DragAbleUIOnDragProgress;
            _dragAbleUI.onDragFinished += DragAbleUIOnDragFinished;
            
            DisplayItem();
        }

        public ItemData ItemData => _itemData;
        public event Action<Vector2, InventoryItemUI> onDragStarted; 
        public event Action<Vector2, InventoryItemUI> onDragProcess; 
        public event Action<Vector2, InventoryItemUI> onDragFinished;

        private void DisplayItem()
        {
            _image.sprite = _itemData.Icon;
            _description.text = _itemData.Description;
            _name.text = _itemData.Name;
            Count = 0;
        }

        private void DragAbleUIOnDragFinished(Vector2 obj)
        {
            
        }

        private void DragAbleUIOnDragProgress(Vector2 obj)
        {
            
        }

        private void DragAbleUIOnDragStarted(Vector2 obj)
        {
            Debug.Log(obj);
        }
        
        public void Dispose()
        {
            //Object.Destroy(_dragAbleUI.gameObject);
            // _dragAbleUI.onDragStarted -= DragAbleUIOnDragStarted;
            //_dragAbleUI.onDragProgress -= DragAbleUIOnDragProgress;
            //_dragAbleUI.onDragFinished -= DragAbleUIOnDragFinished;
        }
    }
}