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
            
            var acceleration = _settings.AsteroidSpeed / _settings.AsteroidAccelerationTime * Time.fixedDeltaTime;
        
            _rb.velocity = Vector3.Project(_rb.velocity, direction);
            _rb.velocity += direction * acceleration;
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _settings.AsteroidSpeed);
            if (_rb.velocity.magnitude < acceleration)
            {
                _rb.velocity = Vector2.zero;
            }
            return ((Vector2)_rb.transform.position - pos).magnitude < .1f;
        }
    }
}