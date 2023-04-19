using Entities;
using Entities.Players;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller: Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
            Container.Bind<Transform>().WithId("Player").FromComponentOnRoot().AsSingle();
            Container.BindInterfacesAndSelfTo<WasdMovementControl>().AsSingle();
            Container.Bind<GroundChecker>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Mover>().AsSingle();
            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
        }
    }
}