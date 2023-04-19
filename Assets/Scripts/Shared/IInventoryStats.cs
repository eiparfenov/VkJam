using System.Collections.Generic;
using Items;

namespace Shared
{
    public interface IInventoryStats
    {
        public List<(ItemData, int)?> CollectedItems { get; }
    }
}