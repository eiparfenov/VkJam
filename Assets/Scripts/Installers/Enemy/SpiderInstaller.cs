using Entities.Enemies;
using Zenject;

namespace Installers.Enemy
{
    public class SpiderInstaller: Installer<SpiderInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Spider>().AsSingle();
        }
    }
}