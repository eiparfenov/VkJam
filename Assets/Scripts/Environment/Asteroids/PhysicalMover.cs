using Installers;
using UnityEngine;
using Zenject;

namespace Environment.Asteroids
{
    public class PhysicalMover: MonoBehaviour, IMover
    {
        private Rigidbody2D _rb;
        private AsteroidSettings _settings;

        [Inject]
        public void Construct(Rigidbody2D rb, AsteroidSettings settings)
        {
            _rb = rb;
            _settings = settings;
        }

        public bool MoveTo(Vector2 pos)
        {
            var direction = pos - (Vector2)transform.position;
            direction = direction.normalized;
            var acceleration = _settings.AsteroidSpeed / _settings.AsteroidAccelerationTime * Time.deltaTime;
            _rb.velocity += acceleration * direction;

            //var crossDirection = (Vector2) Vector3.Cross(direction, Vector3.back);
            //var crossMagnitude = Vector2.Dot(crossDirection, _rb.velocity);
            //if (crossMagnitude > acceleration)
            //{
            //    _rb.velocity -= crossDirection * acceleration;
            //}
            //else
            //{
            //    _rb.velocity -= crossDirection * crossMagnitude;
            //}
            
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _settings.AsteroidSpeed);
            if ((pos - (Vector2)transform.position).magnitude < acceleration)
            {
                _rb.velocity = Vector2.zero;
                _rb.MovePosition(pos);
            }
            return _rb.velocity == Vector2.zero;
        }
    }
}