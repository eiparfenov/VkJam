using System;
using UnityEngine;
using Grid = Grids.Grid;


namespace Environment.Asteroids
{
    [Serializable]
    public class AsteroidCreationData
    {
        [field: SerializeField] public Grid Grid { get; private set; }
    }
}