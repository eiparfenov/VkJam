using System;
using Entities.Enemies.Data;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemySettingsInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private EnemyFactorySettings enemyFactorySettings;
        public override void InstallBindings()
        {
            Container.BindInstance(enemyFactorySettings);
        }
    }

    [Serializable]
    public class EnemyFactorySettings
    {
        [field: SerializeField] public EnemyCreationData[] EnemyCreationRules { get; private set; }
    }
    [Serializable]
    public class EnemyCreationData
    {
        [field: SerializeField] public int EnergyMin { get; private set; }
        [field: SerializeField] public int EnergyMax { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; set; }
        [field: SerializeField] public EnemyData EnemyData { get; set; }
    }
}