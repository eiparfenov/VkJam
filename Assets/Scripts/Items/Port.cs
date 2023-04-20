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

    public static class PortTypeExtension
    {
        public static Port.Type Opposite(this Port.Type port)
        {
            return port switch
            {
                Port.Type.Input => Port.Type.Output,
                Port.Type.Output => Port.Type.Input,
                _ => throw new ArgumentOutOfRangeException(nameof(port), port, null)
            };
        }
    }
}