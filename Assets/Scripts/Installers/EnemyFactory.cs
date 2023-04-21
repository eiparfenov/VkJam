using System.Linq;
using Entities.Enemies.Data;
using Installers.Enemy;
using Shared;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using Entities.Enemies;

namespace Installers
{
    public class EnemyFactory: IFactory<Transform, Vector2Int, Entities.Enemies.Enemy>
    {
        private DiContainer _container;
        private IEnergyStats _energyStats;
        private EnemyFactorySettings _settings;

        public EnemyFactory(DiContainer container, IEnergyStats energyStats, EnemyFactorySettings settings)
        {
            _container = container;
            _energyStats = energyStats;
            _settings = settings;
        }

        public Entities.Enemies.Enemy Create(Transform parent, Vector2Int offset)
        {
            var creationRule = _settings.EnemyCreationRules
                .Where(rule => (rule.EnergyMin <= _energyStats.Energy || rule.EnergyMin == -1) &&
                               (rule.EnergyMax == -1 || _energyStats.Energy <= rule.EnergyMax))
                .RandomOrDefault();
            
            if (creationRule == null) return null;
            
            var enemy = _container.InstantiatePrefab(creationRule.Prefab);
            var context = _container.InstantiateComponent<GameObjectContext>(enemy);
            
            context.Container.BindInterfacesAndSelfTo<EnemyData>().FromInstance(creationRule).AsCached();
            BaseEnemyInstaller.Install(context.Container);
            
            if (creationRule.EnemyData is SpiderData spiderData)
            {
                context.Container.BindInstance(spiderData).AsCached();
                SpiderInstaller.Install(context.Container);
                return context.Container.Resolve<Spider>();
            }

            return null;
        }
    }
}