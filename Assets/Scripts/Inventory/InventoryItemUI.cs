using Items;
using Zenject;

namespace Inventory
{
    public class InventoryItemUI
    {
        public class Factory: PlaceholderFactory<ItemData, InventoryItemUI> { }
        private readonly ItemData _itemData;

        public InventoryItemUI(ItemData itemData)
        {
            _itemData = itemData;
        }
    }
}