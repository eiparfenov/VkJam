using UnityEngine;

namespace Entities
{
    public interface ILookDirectionProvider
    {
        Vector2 LookDirection { get; }
    }
}