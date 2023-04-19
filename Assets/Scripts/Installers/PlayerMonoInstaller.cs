using Entities.Players;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Camera camera;
        public override void InstallBindings()
        {
            Container.BindInstance(camera.transform).WithId("Camera").AsSingle();
            Container.BindInstance(camera).AsSingle();
            Container.Bind<Player>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(player)
                .AsSingle()
                .NonLazy();
        }
    }
}