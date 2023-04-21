using UnityEngine;

namespace Entities
{
    public class MouseLookDirectionProvider: ILookDirectionProvider
    {
        public Vector2 LookDirection => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}