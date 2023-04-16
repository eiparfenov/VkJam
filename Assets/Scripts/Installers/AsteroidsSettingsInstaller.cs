using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "VkJam/Installers/Settings/Asteroid", fileName = "AsteroidSettings")]
    public class AsteroidsSettingsInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private AsteroidSettings asteroidSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(asteroidSettings).AsSingle();
        }
    }

    [Serializable]
    public class AsteroidSettings
    {
        [field: SerializeField] public float AsteroidAccelerationTime { get; set; }
        [field: SerializeField] public float AsteroidSpeed { get; set; }
    }
}