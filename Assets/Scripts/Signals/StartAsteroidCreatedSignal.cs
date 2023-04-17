using Environment.Asteroids;

namespace Signals
{
    public class StartAsteroidCreatedSignal: ISignal
    {
        public Asteroid StartAsteroid { get; set; }
    }
}