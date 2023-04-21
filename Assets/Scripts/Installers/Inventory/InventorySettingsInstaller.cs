using System;
using UnityEngine;
using Zenject;

namespace Installers.Inventory
{
    [CreateAssetMenu(menuName = "VkJam/Installers/Settings/Inventory", fileName = "InventorySettings")]
    public class InventorySettingsInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private InventorySettings inventorySettings;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<InventorySettings>()
                .FromInstance(inventorySettings)
                .AsSingle();
        }
    }

    [Serializable]
    public class InventorySettings: IInventoryCellUiSettings, IInventoryControllerSettings, IInventoryItemUISettings
    {
        [field: SerializeField] public Color Clear      { get; private set; }
        [field: SerializeField] public Color CanPlace   { get; private set; }
        [field: SerializeField] public Color CantPlace  { get; private set; }
        [field: SerializeField] public Vector2Int Size  { get; private set; }
        [field: SerializeField] public Vector2 CellSize { get; private set; }
    }
    public interface IInventoryCellUiSettings
    {
        Color Clear { get; }
        Color CanPlace { get; }
        Color CantPlace { get; }
    }

    public interface IInventoryControllerSettings
    {
        Vector2Int Size { get; }
    }

    public interface IInventoryItemUISettings
    {
        Vector2 CellSize { get; }
    }
}