using UnityEngine;

namespace Entities.Players
{
    public class WasdMovementControl: IMovementControl
    {
        public Vector2 Direction
        {
            get
            {
                var result = Vector2.zero;
                if(Input.GetKey(KeyCode.W)) result += Vector2.up;
                if(Input.GetKey(KeyCode.A)) result += Vector2.left;
                if(Input.GetKey(KeyCode.S)) result += Vector2.down;
                if(Input.GetKey(KeyCode.D)) result += Vector2.right;

                return result.normalized;
            }
        }
    }
}