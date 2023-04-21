using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class DragAbleUI: MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
    {
        public event Action<Vector2> onDragStarted;
        public event Action<Vector2> onDragProgress;
        public event Action<Vector2> onDragFinished;

        public void OnBeginDrag(PointerEventData eventData)
        {
            onDragStarted?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDragProgress?.Invoke(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onDragFinished?.Invoke(eventData.position);
        }
    }
}