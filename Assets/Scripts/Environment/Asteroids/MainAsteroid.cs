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
        }


        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        private void AsteroidOnStopMovement(AsteroidStoppedSignal signal)
        {
            var sides = _mainGrid.Sides(signal.GridSelf, signal.Offset);
            if (sides == 0) return;
            
            
        }
    }
}