using System.Collections.Generic;
using System.Linq;
using Installers;
using UnityEngine;
using Zenject;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Entities
{
    public class Mover: IFixedTickable
    {
        private readonly float _maxSpeed;
        private readonly float _accelerationTime;
        private readonly Rigidbody2D _rb;
        private readonly IMovementControl _control;
        private readonly List<GroundChecker> _groundCheckers;

        public Mover(Rigidbody2D rb, IMovementControl control, PlayerSettings settings, List<GroundChecker> groundCheckers)
        {
            _rb = rb;
            _control = control;
            _maxSpeed = settings.MaxSpeed;
            _accelerationTime = settings.AccelerationTime;
            _groundCheckers = groundCheckers;
        }

        public void FixedTick()
        {
            var dir = _control.Direction;
            var acceleration = _maxSpeed / _accelerationTime * Time.fixedDeltaTime;
            if (dir.magnitude != 0)
            {
                _rb.velocity = Vector3.Project(_rb.velocity, _control.Direction);
                _rb.velocity += dir * acceleration;
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
            }
            else
            {
                if (_rb.velocity.magnitude > acceleration)
                {
                    _rb.velocity -= _rb.velocity.normalized * acceleration;
                }
                else
                {
                    _rb.velocity = Vector2.zero;
                }
            }

            var groundCheckersToCalculate = _groundCheckers.Where(
                checker => !checker.IsGrounded && Vector2.Dot(_rb.velocity, checker.Direction) < 0);

            foreach (var groundChecker in groundCheckersToCalculate)
            {
                _rb.velocity -= (Vector2) Vector3.Project(_rb.velocity, groundChecker.Direction);
            }
        }
    }
}