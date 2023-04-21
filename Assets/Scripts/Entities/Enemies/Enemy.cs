using System;
using Cysharp.Threading.Tasks;
using Entities.Players;
using UnityEngine;
using Utils.Extensions;

namespace Entities.Enemies
{
    public abstract class Enemy
    {
        private readonly Player _player;
        private readonly IPathFinder _pathFinder;
        private readonly EnemyMovementControl _enemyMovementControl;
        protected readonly Transform transform;

        protected Enemy(Player player, IPathFinder pathFinder, EnemyMovementControl enemyMovementControl, Transform transform)
        {
            _player = player;
            _pathFinder = pathFinder;
            _enemyMovementControl = enemyMovementControl;
            this.transform = transform;
        }

        protected async UniTask FollowPlayer(Predicate<FollowingInformation> breakPredicate)
        {
            while (!breakPredicate(new FollowingInformation()
                       { Player = _player.Position, Self = transform.position }))
            {
                var currentPosition = ((Vector2)transform.position).ToCellPosition();
                var playerPosition = ((Vector2)_player.Position).ToCellPosition();
                var direction = _pathFinder.NextCell(currentPosition, playerPosition);
                _enemyMovementControl.Direction = direction ?? Vector2.zero;
            }
        }
    }

    public class FollowingInformation
    {
        public Vector2 Player { get; set; }
        public Vector2 Self { get; set; }
    }
}