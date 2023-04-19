using System;
using UnityEngine;
using Zenject;

namespace Environment.Asteroids
{
    public class DragAble: MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _offset;
        private Vector2 _target;
        private bool _isDragging;

        public Vector2 Target => _target;
        public bool IsDragging => _isDragging;
        public event Action onDragStart;

        [Inject]
        public void Construct(Rigidbody2D rb)
        {
            _rb = rb;
        }

        private void OnMouseDown()
        {
            _isDragging = true;
            _offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _target = transform.position;
            onDragStart?.Invoke();
        }

        private void OnMouseDrag()
        {
            _target = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _offset;
        }

        private void OnMouseUp()
        {
            _isDragging = false;
        }
    }
}