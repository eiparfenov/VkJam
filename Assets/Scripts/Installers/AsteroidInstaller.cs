using Environment.Asteroids;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Installers
{
    public class AsteroidInstaller: Installer<AsteroidInstaller>
    {
        private readonly AsteroidCreationData _asteroidCreationData;

        public AsteroidInstaller(AsteroidCreationData asteroidCreationData)
        {
            _asteroidCreationData = asteroidCreationData;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_asteroidCreationData);
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
            Container.Bind<DragAble>().FromNewComponentOnRoot().AsSingle();
            Container.Bind<IMover>().To<PhysicalMover>().FromNewComponentOnRoot().AsSingle();
            Container.Bind<Tilemap>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<Asteroid>().AsSingle();
        }
    }
}