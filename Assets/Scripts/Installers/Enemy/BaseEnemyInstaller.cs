using Entities;
using Entities.Players;
using UnityEngine;
using Zenject;

namespace Installers.Enemy
{
    public class BaseEnemyInstaller: Installer<BaseEnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
            Container.Bind<Transform>().WithId("Enemy").FromComponentOnRoot().AsCached();
            Container.Bind<SpriteRenderer>().FromComponentInHierarchy().AsCached();
            Container.Bind<ILookDirectionProvider>().To<MouseLookDirectionProvider>().AsCached();
            Container.BindInterfacesAndSelfTo<LookRotator>().AsCached();
            Container.BindInterfacesAndSelfTo<WasdMovementControl>().AsSingle();
            Container.Bind<GroundChecker>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<Mover>().AsSingle();
        }
    }
}