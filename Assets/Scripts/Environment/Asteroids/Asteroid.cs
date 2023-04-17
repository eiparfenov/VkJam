using System;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;
using Zenject;
using Grid = Grids.Grid;

namespace Environment.Asteroids
{
    public class Asteroid: IInitializable, IDisposable
    {
        public class Factory: PlaceholderFactory<AsteroidCreationData, Asteroid>{}
        private readonly DragAble _dragAble;
        private readonly IMover _mover;
        private readonly Tilemap _tilemap;
        private readonly TileBase _asteroidTile;
        private readonly AsteroidCreationData _creationData;
        private readonly Transform _transform;
        private readonly SignalBus _signalBus;
        private bool _isPlaced;

        public Vector2Int Position => ((Vector2)_transform.position).RoundToVector2Int();
        public Grid GridSelf => _creationData.Grid;
        
        public Asteroid(DragAble dragAble, IMover mover, Tilemap tilemap, TileBase asteroidTile, AsteroidCreationData creationData, Transform transform, SignalBus signalBus)
        {
            _dragAble = dragAble;
            _mover = mover;
            _tilemap = tilemap;
            _asteroidTile = asteroidTile;
            _creationData = creationData;
            _transform = transform;
            _signalBus = signalBus;

            _transform.position = creationData.StartPos.ToVector3Int();
        }

        public bool IsPlaced
        {
            get => _isPlaced;
            set
            {
                _dragAble.enabled = false;
                _isPlaced = value;
            }
        }

        public void Initialize()
        {
            _dragAble.onDragStart += DragHandler;

            var grid = _creationData.Grid;
            foreach (var gridCell in grid.AllCells)
            {
                _tilemap.SetTile(gridCell.ToVector3Int() ,_asteroidTile);
            }
        }

        private async void DragHandler()
        {
            while (_dragAble.IsDragging)
            {
                _mover.MoveTo(_dragAble.Target);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            while (!_dragAble.IsDragging && !_mover.MoveTo(((Vector2)_transform.position).RoundToInt()))
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (!_dragAble.IsDragging)
            {
                _signalBus.Fire(new AsteroidStoppedSignal()
                {
                    GridSelf = _creationData.Grid,
                    Invoker = this,
                    Offset = ((Vector2)_transform.position).RoundToVector2Int()
                });
            }
        }

        public void Dispose()
        {
            _dragAble.onDragStart -= DragHandler;
        }
    }
}