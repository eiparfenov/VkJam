using System;
using UnityEngine;
using Utils;

namespace Items
{
    [Serializable]
    public class Port
    {
        [field: SerializeField] public Vector2Int Position { get; private set; }
        [field: SerializeField] public Direction Direction { get; private set; }
        [field: SerializeField] public Type PortType { get; private set; }
        [field: SerializeField] public PortColor PortColor { get; private set; }
        public enum Type
        {
            Input, Output
        }
    }
}