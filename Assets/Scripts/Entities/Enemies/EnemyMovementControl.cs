using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyMovementControl: IMovementControl
    {
        public Vector2 Direction { get; set; }
    }
}