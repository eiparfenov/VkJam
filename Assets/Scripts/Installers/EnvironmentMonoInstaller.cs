using System;
using Environment.Asteroids;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Grid = Grids.Grid;

namespace Installers
{
    public class EnvironmentMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject asteroidPref;
        [SerializeField] private AsteroidCreationData asteroidCreationData;
        [SerializeField] private TileBase asteroidTile;
        public override void InstallBindings()
        {
            //var grid1 = new Grid(new[]
            //{
            //    new Vector2Int(0, 0)
            //});
            //var grid2 = new Grid(new[]
            //{
            //    new Vector2Int(0, 1)
            //});
            //var offset = new Vector2Int(0, -1);
            //print(grid1.PossibleToAdd(Grid));
            Container.BindInstance(asteroidTile).AsSingle().WhenInjectedInto<Asteroid>();
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