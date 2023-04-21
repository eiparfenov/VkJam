using UnityEngine;

namespace Environment.Asteroids
{
    public interface IMover
    {
        bool MoveTo(Vector2 pos);
    }
}