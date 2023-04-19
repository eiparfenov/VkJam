using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "VkJam/Installers/Settings/Player", fileName = "PlayerSettings")]
    public class PlayerSettingsInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private PlayerSettings playerSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerSettings).AsSingle();
        }
    }

    [Serializable]
    public class PlayerSettings
    {
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float AccelerationTime { get; private set; }
        [field: SerializeField] public int MinCameraSize { get; private set; }
        [field: SerializeField] public int MaxCameraSize { get; private set; }
    }
}