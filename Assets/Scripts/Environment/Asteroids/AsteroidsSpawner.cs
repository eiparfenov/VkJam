using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Installers;
using Signals;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using Grid = Grids.Grid;
using Random = UnityEngine.Random;

namespace Environment.Asteroids
{
    public class AsteroidsSpawner: IInitializable, IDisposable
    {
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly AsteroidSpawnerSettings _settings;
        private readonly SignalBus _signalBus;
        private readonly List<Asteroid> _asteroids;

        private readonly CancellationTokenSource _tokenSource;
        private bool _isWorking;

        public AsteroidsSpawner(Asteroid.Factory asteroidFactory, AsteroidSpawnerSettings settings, SignalBus signalBus)
        {
            _asteroidFactory = asteroidFactory;
            _asteroids = new List<Asteroid>();
            _signalBus = signalBus;
            _settings = settings;
            
            _signalBus.Subscribe<PlayerMoveSignal>(CreateAsteroid);
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            var asteroidFirst = _asteroidFactory.Create(new AsteroidCreationData()
            {
                Grid = Grid.RandomByCellsCount(_settings.StartAsteroidSize),
                StartPos = Vector2Int.zero
            });
            asteroidFirst.IsPlaced = true;
            _asteroids.Add(asteroidFirst);
            _signalBus.Fire(new StartAsteroidCreatedSignal(){StartAsteroid = asteroidFirst});
        }

        private async void CreateAsteroid(PlayerMoveSignal signal)
        {
            if(_isWorking) return;
            _isWorking = true;
            var asteroidGrid = Grid.RandomByCellsCount(Random.Range(_settings.CreatedAsteroidMinSize, _settings.CreatedAsteroidMaxSize));
            
            foreach (var fieldToAdd in signal.FoV.Rect().Select(x => x + signal.Position))
            {
                if(_tokenSource.Token.IsCancellationRequested) return;
                if (_asteroids.Select(asteroid =>
                        asteroid.GridSelf.PossibleToAddWithSide(asteroidGrid, fieldToAdd - asteroid.Position, _settings.SidesOffset))
                .All(x => x))
                {
                    var asteroid = _asteroidFactory.Create(new AsteroidCreationData()
                        { StartPos = fieldToAdd, Grid = asteroidGrid });
                    _asteroids.Add(asteroid);
                    asteroidGrid = Grid.RandomByCellsCount(Random.Range(_settings.CreatedAsteroidMinSize, _settings.CreatedAsteroidMaxSize));
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            _isWorking = false;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerMoveSignal>(CreateAsteroid);
            _tokenSource.Cancel();
        }
    }
}