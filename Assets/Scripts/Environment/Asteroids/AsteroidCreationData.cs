using System;
using UnityEngine;
using Grid = Grids.Grid;


namespace Environment.Asteroids
{
    [Serializable]
    public class AsteroidCreationData
    {
        [field: SerializeField] public Grid Grid { get; set; }
        public Vector2Int StartPos { get; set; }
    }
}