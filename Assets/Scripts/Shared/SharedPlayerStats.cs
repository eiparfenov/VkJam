using System.Collections.Generic;
using Installers;
using Items;

namespace Shared
{
    public class SharedPlayerStats: IInventoryStats
    {
        public SharedPlayerStats(PlayerSettings playerSettings)
        {
            CollectedItems = new List<(ItemData, int)?>();
            foreach (var startItem in playerSettings.StartItems)
            {
                CollectedItems.Add((startItem.ItemData, startItem.Count));
            }
        }

        public List<(ItemData, int)?> CollectedItems { get; private set; }
    }
}