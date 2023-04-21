using Entities.Players;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    public class Spider: Enemy, IInitializable
    {
        public Spider(Player player, IPathFinder pathFinder, EnemyMovementControl enemyMovementControl, Transform transform) : base(player, pathFinder, enemyMovementControl, transform)
        {
        }

        public async void Initialize()
        {
            await FollowPlayer(x => false);
        }
    }
}