using System.Collections.Generic;
using System.Linq;
using Installers;
using Items;

namespace Shared
{
    public class SharedPlayerStats: IInventoryStats, IEnergyStats
    {
        public SharedPlayerStats(PlayerSettings playerSettings)
        {
            CollectedItems = new List<InventorySlot>();
            foreach (var startItem in playerSettings.StartItems)
            {
                CollectedItems.Add(new InventorySlot(){Count = startItem.Count, ItemData = startItem.ItemData});
            }
        }

        public List<InventorySlot> CollectedItems { get; private set; }
        public void AddItem(ItemData itemData)
        {
            var slot = CollectedItems.FirstOrDefault(x => x.ItemData == itemData);
            if (slot == null)
            {
                CollectedItems.Add(new InventorySlot(){Count = 1, ItemData = itemData});
            }
            else
            {
                slot.Count += 1;
            }
        }

        public void RemoveItem(ItemData itemData)
        {
            var slot = CollectedItems.FirstOrDefault(x => x.ItemData == itemData);
            if(slot == null) return;
            slot.Count -= 1;
            if (slot.Count == 0)
            {
                CollectedItems.Remove(slot);
            }
        }

        public int Energy { get; set; }
    }
}