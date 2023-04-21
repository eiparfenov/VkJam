using System;
using Entities;
using Items;
using Shared;
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
            Container.BindInterfacesAndSelfTo<PlayerSettings>().FromInstance(playerSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<SharedPlayerStats>().AsSingle();
        }
    }

    [Serializable]
    public class PlayerSettings: IMovementSettings
    {
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float AccelerationTime { get; private set; }
        [field: SerializeField] public int MinCameraSize { get; private set; }
        [field: SerializeField] public int MaxCameraSize { get; private set; }
        [field: SerializeField] public InventoryItemAndCount[] StartItems { get; private set; }
        [Serializable]
        public class InventoryItemAndCount
        {
            [field: SerializeField] public int Count { get; private set; }
            [field: SerializeField] public ItemData ItemData { get; private set; }
        }
    }
}