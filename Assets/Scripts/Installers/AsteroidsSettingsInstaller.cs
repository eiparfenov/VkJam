using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "VkJam/Installers/Settings/Asteroid", fileName = "AsteroidSettings")]
    public class AsteroidsSettingsInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private AsteroidSettings asteroidSettings;
        [SerializeField] private AsteroidSpawnerSettings asteroidSpawnerSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(asteroidSettings).AsSingle();
            Container.BindInstance(asteroidSpawnerSettings).AsSingle();
        }
    }

    [Serializable]
    public class AsteroidSettings
    {
        [field: SerializeField] public float AsteroidAccelerationTime { get; set; }
        [field: SerializeField] public float AsteroidSpeed { get; set; }
    }

    [Serializable]
    public class AsteroidSpawnerSettings
    {
        [field: SerializeField] public Vector2Int FieldSize { get; private set; }
        [field: SerializeField] public int SidesOffset { get; private set; }
        [field: SerializeField] public int StartAsteroidSize { get; private set; }
        [field: SerializeField] public Vector2Int LandingSite { get; private set; }
        [field: SerializeField] public int CreatedAsteroidMinSize { get; private set; }
        [field: SerializeField] public int CreatedAsteroidMaxSize { get; private set; }
        [field: SerializeField] public int FreeAsteroidsCount { get; private set; }
    }
}