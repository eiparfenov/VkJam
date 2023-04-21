using UnityEngine;

namespace Entities
{
    public interface IMovementControl
    {
        Vector2 Direction { get; }
    }
}