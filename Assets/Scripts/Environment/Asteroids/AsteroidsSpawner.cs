using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Installers;
using Signals;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using Grid = Grids.Grid;

namespace Environment.Asteroids
{
    public class AsteroidsSpawner: IInitializable
    {
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly AsteroidSpawnerSettings _settings;
        private readonly SignalBus _signalBus;
        private readonly List<Asteroid> _asteroids;

        public AsteroidsSpawner(Asteroid.Factory asteroidFactory, AsteroidSpawnerSettings settings, SignalBus signalBus)
        {
            _asteroidFactory = asteroidFactory;
            _asteroids = new List<Asteroid>();
            _signalBus = signalBus;
            _settings = settings;
        }

        public async void Initialize()
        {
            var asteroidFirst = _asteroidFactory.Create(new AsteroidCreationData()
            {
                Grid = Grid.RandomByCellsCount(_settings.StartAsteroidSize),
                StartPos = _settings.FieldSize
            });
            _asteroids.Add(asteroidFirst);
            _signalBus.Fire(new StartAsteroidCreatedSignal(){StartAsteroid = asteroidFirst});
            await CreateAsteroid();
        }

        private async UniTask CreateAsteroid()
        {
            var asteroidGrid = Grid.RandomByCellsCount(Random.Range(_settings.CreatedAsteroidMinSize, _settings.CreatedAsteroidMaxSize));
            while(true)
            {
                foreach (var fieldToAdd in (_settings.FieldSize * 2).AllInside())
                {
                    if (_asteroids.Select(asteroid =>
                            asteroid.GridSelf.PossibleToAddWithSide(asteroidGrid, fieldToAdd - asteroid.Position, _settings.SidesOffset))
                    .All(x => x))
                    {
                        var asteroid = _asteroidFactory.Create(new AsteroidCreationData()
                            { StartPos = fieldToAdd, Grid = asteroidGrid });
                        _asteroids.Add(asteroid);
                        asteroidGrid = Grid.RandomByCellsCount(Random.Range(_settings.CreatedAsteroidMinSize, _settings.CreatedAsteroidMaxSize));
                    }
                }
                await UniTask.Delay(1000);
            }
        }
    }
}