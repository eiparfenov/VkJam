using UnityEngine;

namespace Entities.Enemies.Data
{
    public abstract class EnemyData: ScriptableObject
    {
        [filed: SerializeField] public float MaxSpeed { get; private set; }
        [filed: SerializeField] public float MaxDamage { get; private set; }
        [filed: SerializeField] public float MaxHealth { get; private set; }
    }
}