using UnityEngine;

namespace Environment.Asteroids
{
    public class SimpleMover:MonoBehaviour, IMover
    {
        public bool MoveTo(Vector2 pos)
        {
            transform.position = pos;
            return true;
        }
    }
}