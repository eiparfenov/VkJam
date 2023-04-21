using Environment.Asteroids;
using Zenject;

namespace Installers
{
    public class AsteroidFactory: IFactory<AsteroidCreationData, Asteroid>
    {
        private DiContainer _container;

        public AsteroidFactory(DiContainer container)
        {
            _container = container;
        }

        public Asteroid Create(AsteroidCreationData creationData)
        {
            var subContainer = _container.CreateSubContainer();
            return null;
        }
        
    }
}