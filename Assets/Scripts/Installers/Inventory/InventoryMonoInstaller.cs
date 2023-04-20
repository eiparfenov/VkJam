using Inventory;
using Items;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers.Inventory
{
    public class InventoryMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject itemRow;
        [SerializeField] private GameObject inventoryCell;

        [Space] 
        [SerializeField] private RectTransform cellsGrid;
        [SerializeField] private Transform itemRows;
        [SerializeField] private Transform inventoryRootObject;
        [SerializeField] private Transform itemDragContainer;
        [SerializeField] private Transform itemPlaceContainer;
        public override void InstallBindings()
        {
            Container.BindFactory<ItemData, InventoryItemRowUI, InventoryItemRowUI.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<InventoryItemRowUIInstaller>(itemRow)
                .UnderTransform(itemRows);
            Container.BindFactory<InventoryCellUI, InventoryCellUI.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<InventoryCellUIInstaller>(inventoryCell)
                .UnderTransform(cellsGrid);
            Container.BindFactory<ItemData, InventoryItemUI, InventoryItemUI.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<InventoryItemUIInstaller>(inventoryCell)
                .UnderTransform(inventoryRootObject);
            
            Container.BindInstance(cellsGrid).WithId("CellsGrid").AsCached();
            Container.BindInstance(itemDragContainer).WithId("DragContainer").AsCached();
            Container.BindInstance(itemPlaceContainer).WithId("PlaceContainer").AsCached();
            Container.BindInterfacesAndSelfTo<InventoryProcessor>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsGridController>().AsSingle();
            Container.BindInstance(inventoryRootObject).AsCached().WhenInjectedInto<InventoryController>();
            Container.BindInterfacesAndSelfTo<InventoryController>()
                .AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TempInventoryOpener>().AsSingle().NonLazy();
        }
    }
}