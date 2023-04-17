using System;
using Grids;
using Signals;
using Zenject;

namespace Environment.Asteroids
{
    public class MainAsteroid: IInitializable, IDisposable
    {
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly SignalBus _signalBus;
        private Grid _mainGrid;
        public MainAsteroid(Asteroid.Factory asteroidFactory, SignalBus signalBus)
        {
            _asteroidFactory = asteroidFactory;
            _signalBus = signalBus;
            _mainGrid = new Grid();
        }
        
        public async void Initialize()
        {
            _signalBus.Subscribe<AsteroidStoppedSignal>(AsteroidOnStopMovement);
            _signalBus.Subscribe<StartAsteroidCreatedSignal>(AsteroidSpawnerOnStartAsteroidCreated);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<AsteroidStoppedSignal>(AsteroidOnStopMovement);
            _signalBus.Unsubscribe<StartAsteroidCreatedSignal>(AsteroidSpawnerOnStartAsteroidCreated);
        }

        public void AsteroidSpawnerOnStartAsteroidCreated(StartAsteroidCreatedSignal signal)
        {
            _mainGrid.UnionWith(signal.StartAsteroid.GridSelf, signal.StartAsteroid.Position);
        }

        private void AsteroidOnStopMovement(AsteroidStoppedSignal signal)
        {
            var sides = _mainGrid.Sides(signal.GridSelf, signal.Offset);
            if (sides == 0) return;
            
            _signalBus.Fire(new AsteroidAddToMainSignal(){Energy = sides});
            signal.Invoker.IsPlaced = true;
        }
    }
}