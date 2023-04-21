using Inventory;
using Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers.Inventory
{
    public class InventoryItemUIInstaller: Installer<ItemData, InventoryItemUIInstaller>
    {
        private readonly ItemData _itemData;

        public InventoryItemUIInstaller(ItemData itemData)
        {
            _itemData = itemData;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_itemData).AsSingle();
            Container.Bind<RectTransform>().FromComponentOnRoot().AsSingle();
            Container.Bind<DragAbleUI>().FromNewComponentOnRoot().AsSingle();
            Container.Bind<Image>().FromComponentOnRoot().AsSingle();
            Container.Bind<InventoryItemUI>().AsSingle();
        }
    }
}