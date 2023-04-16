using System;
using Environment.Asteroids;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Installers
{
    public class EnvironmentMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject asteroidPref;
        [SerializeField] private AsteroidCreationData asteroidCreationData;
        [SerializeField] private TileBase asteroidTile;
        public override void InstallBindings()
        {
            Container.BindInstance(asteroidTile).AsSingle().WhenInjectedInto<Asteroid>();
            Container.BindFactory<AsteroidCreationData, Asteroid, Asteroid.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<AsteroidInstaller>(asteroidPref)
                .WithGameObjectName("Asteroid")
                .UnderTransformGroup("Asteroids")
                .AsSingle();

            Container.Resolve<Asteroid.Factory>().Create(asteroidCreationData);
        }
    }
}