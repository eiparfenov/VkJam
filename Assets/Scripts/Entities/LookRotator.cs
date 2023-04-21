using Entities.Players;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class LookRotator: ITickable
    {
        private readonly ILookDirectionProvider _lookDirection;
        private readonly Transform _transform;

        public LookRotator(ILookDirectionProvider lookDirection, SpriteRenderer renderer)
        {
            _lookDirection = lookDirection;
            _transform = renderer.transform;
        }

        public void Tick()
        {
            _transform.localRotation = Quaternion.Euler(0, 0, 
                Vector3.SignedAngle(
                    Vector3.right, 
                    (Vector3)_lookDirection.LookDirection - _transform.position,
                    Vector3.forward
                    )
                );
        }
    }
}