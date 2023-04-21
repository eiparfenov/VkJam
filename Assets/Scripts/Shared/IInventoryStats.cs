using System.Collections.Generic;
using Items;

namespace Shared
{
    public interface IInventoryStats
    {
        List<InventorySlot> CollectedItems { get; }
        void AddItem(ItemData itemData);
        void RemoveItem(ItemData itemData);
    }

    public class InventorySlot
    {
        public ItemData ItemData { get; set; }
        public int Count { get; set; }
    }
}