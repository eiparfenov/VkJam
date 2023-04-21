using Entities;
using Entities.Players;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using Zenject;

namespace Installers
{
    public class PlayerInstaller: Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
            Container.Bind<Transform>().WithId("Player").FromComponentOnRoot().AsCached();
            Container.Bind<SpriteRenderer>().FromComponentInHierarchy().AsCached();
            Container.Bind<ILookDirectionProvider>().To<MouseLookDirectionProvider>().AsCached();
            Container.BindInterfacesAndSelfTo<LookRotator>().AsCached();
            Container.BindInterfacesAndSelfTo<WasdMovementControl>().AsSingle();
            Container.Bind<GroundChecker>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Mover>().AsSingle();
            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
        }
    }
}