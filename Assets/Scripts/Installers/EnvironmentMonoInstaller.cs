using System;
using Environment.Asteroids;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;
using Zenject;
using Grid = Grids.Grid;

namespace Installers
{
    public class EnvironmentMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject asteroidPref;
        [SerializeField] private RandomTile asteroidTile;
        [SerializeField] private TileBase backgroundTile;
        [SerializeField] private TileBase physicTile;
        public override void InstallBindings()
        {
            Container.BindInstance(asteroidTile).AsSingle().WhenInjectedInto<Asteroid>();
            Container.BindInstance(backgroundTile).WithId("Background").AsCached().WhenInjectedInto<Asteroid>();
            Container.BindInstance(physicTile).WithId("Physic").AsCached().WhenInjectedInto<Asteroid>();
            Container.BindFactory<AsteroidCreationData, Asteroid, Asteroid.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<AsteroidInstaller>(asteroidPref)
                .WithGameObjectName("Asteroid")
                .UnderTransformGroup("Asteroids")
                .AsSingle();

            Container.BindInterfacesAndSelfTo<MainAsteroid>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AsteroidsSpawner>().AsSingle().NonLazy();
        }
    }
}