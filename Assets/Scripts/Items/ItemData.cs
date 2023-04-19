using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "VkJam/Items/BaseItem", fileName = "Item")]
    public class ItemData: ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Port[] Ports { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}