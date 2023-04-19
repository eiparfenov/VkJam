using System.Linq;
using UnityEngine;

namespace Entities
{
    public class GroundChecker: MonoBehaviour
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
        public bool IsGrounded => Physics2D.OverlapCircleAll(transform.position, .1f).Any(x => x.CompareTag("Ground"));
    }
}