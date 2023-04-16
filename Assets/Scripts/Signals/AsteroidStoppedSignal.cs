using Environment.Asteroids;
using UnityEngine;
using Grid = Grids.Grid;

namespace Signals
{
    public class AsteroidStoppedSignal: ISignal
    {
        public Grid GridSelf { get; set; }
        public Vector2Int Offset { get; set; }
        public Asteroid Invoker { get; set; }
    }
}