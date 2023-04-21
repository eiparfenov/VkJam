using UnityEngine;

namespace Signals
{
    public class PlayerMoveSignal: ISignal
    {
        public Vector2Int Position { get; set; }
        public Vector2Int FoV { get; set; }
    }
}