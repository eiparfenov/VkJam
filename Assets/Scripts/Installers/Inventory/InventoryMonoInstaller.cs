using Inventory;
using Items;
using UnityEngine;
using Zenject;

namespace Installers.Inventory
{
    public class InventoryMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject itemRow;
        [SerializeField] private Transform itemRows;
        [SerializeField] private Transform inventoryRootObject;
        public override void InstallBindings()
        {
            Container.BindFactory<ItemData, InventoryItemRowUI, InventoryItemRowUI.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<InventoryItemRowUIInstaller>(itemRow)
                .UnderTransform(itemRows);
            Container.BindInstance(inventoryRootObject).AsCached().WhenInjectedInto<InventoryController>();
            Container.BindInterfacesAndSelfTo<InventoryController>()
                .AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TempInventoryOpener>().AsSingle().NonLazy();
        }
    }
}