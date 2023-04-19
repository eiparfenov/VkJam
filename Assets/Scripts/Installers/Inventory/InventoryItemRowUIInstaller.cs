using Inventory;
using Items;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace Installers.Inventory
{
    public class InventoryItemRowUIInstaller: Installer<ItemData, InventoryItemRowUIInstaller>
    {
        private readonly ItemData _itemData;

        public InventoryItemRowUIInstaller(ItemData itemData)
        {
            _itemData = itemData;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_itemData);
            Container.Bind<Image>().FromComponentsInHierarchy().AsCached();
            Container.Bind<TextMeshProUGUI>().FromComponentsInHierarchy().AsCached();
            Container.Bind<DragAbleUI>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InventoryItemRowUI>().AsSingle();
        }
    }
}