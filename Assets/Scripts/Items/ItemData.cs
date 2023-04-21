using NaughtyAttributes;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "VkJam/Items/BaseItem", fileName = "Item")]
    public class ItemData: ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Port[] Ports { get; private set; }
        [field: SerializeField] public Vector2Int[] Cells { get; private set; }

        [Button()]
        private void MakeOneCell()
        {
            Cells = new[] { Vector2Int.zero };
        }
    }
}