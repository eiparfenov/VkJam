using Inventory;
using UnityEngine.UI;
using Zenject;

namespace Installers.Inventory
{
    public class InventoryCellUIInstaller: Installer<InventoryCellUIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Image>().FromComponentOnRoot().AsSingle();
            Container.Bind<InventoryCellUI>().AsSingle();
        }
    }
}