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
        private int _currentAsteroidsCount;
        private Vector2Int _playerFov;
        private Vector2Int _playerPosition;

        public AsteroidsSpawner(Asteroid.Factory asteroidFactory, AsteroidSpawnerSettings settings, SignalBus signalBus)
        {
            _asteroidFactory = asteroidFactory;
            _asteroids = new List<Asteroid>();
            _signalBus = signalBus;
            _settings = settings;
            
            _tokenSource = new CancellationTokenSource();
            _signalBus.Subscribe<PlayerMoveSignal>(OnPlayerMove);
            _signalBus.Subscribe<AsteroidAddToMainSignal>(OnAsteroidAttached);
        }

        public async void Initialize()
        {
            var gridFirst = Grid.RandomByCellsCount(_settings.StartAsteroidSize);
            var gridLandingSpace = new Grid(_settings.LandingSite.AllInside());
            gridFirst.UnionWith(gridLandingSpace, -Vector2Int.one);
            var asteroidFirst = _asteroidFactory.Create(new AsteroidCreationData()
            {
                Grid = gridFirst,
                StartPos = Vector2Int.zero
            });
            asteroidFirst.IsPlaced = true;
            _asteroids.Add(asteroidFirst);
            _signalBus.Fire(new StartAsteroidCreatedSignal(){StartAsteroid = asteroidFirst});

            while (!_tokenSource.Token.IsCancellationRequested)
            {
                await UniTask.WaitWhile(() => _settings.FreeAsteroidsCount == _currentAsteroidsCount);
                await CreateAsteroid();
            }
        }

        private async UniTask CreateAsteroid()
        {
            var asteroidGrid = Grid.RandomByCellsCount(Random.Range(_settings.CreatedAsteroidMinSize, _settings.CreatedAsteroidMaxSize));
            
            foreach (var fieldToAdd in _playerFov.Rect().Select(x => x + _playerPosition).OrderBy(x => x.sqrMagnitude))
            {
                if(_tokenSource.Token.IsCancellationRequested) return;
                if (_asteroids.Select(asteroid =>
                        asteroid.GridSelf.PossibleToAddWithSide(asteroidGrid, fieldToAdd - asteroid.Position, _settings.SidesOffset))
                .All(x => x))
                {
                    var asteroid = _asteroidFactory.Create(new AsteroidCreationData()
                        { StartPos = fieldToAdd, Grid = asteroidGrid });
                    _asteroids.Add(asteroid);
                    _currentAsteroidsCount += 1;
                    break;
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private void OnPlayerMove(PlayerMoveSignal signal)
        {
            _playerFov = signal.FoV;
            _playerPosition = signal.Position;
        }

        private void OnAsteroidAttached(AsteroidAddToMainSignal signal)
        {
            _currentAsteroidsCount -= 1;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerMoveSignal>(OnPlayerMove);
            _signalBus.Unsubscribe<AsteroidAddToMainSignal>(OnAsteroidAttached);
            _tokenSource.Cancel();
        }
    }
}