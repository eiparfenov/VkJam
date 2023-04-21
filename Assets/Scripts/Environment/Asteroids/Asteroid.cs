using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;
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
        private readonly Tilemap _tilemapPhysic;
        private readonly Tilemap _tilemapBackground;
        private readonly Tilemap _tilemapMain;
        private readonly TileBase _physicTile;
        private readonly TileBase _backgroundTile;
        private readonly RandomTile _asteroidTile;
        private readonly AsteroidCreationData _creationData;
        private readonly Transform _transform;
        private readonly SignalBus _signalBus;
        private readonly Rigidbody2D _rb;
        private bool _isPlaced;

        public Vector2Int Position => ((Vector2)_transform.position).RoundToVector2Int();
        public Grid GridSelf => _creationData.Grid;

        public Asteroid(DragAble dragAble, 
            IMover mover, 
            Tilemap[] tilemaps, 
            RandomTile asteroidTile,
            AsteroidCreationData creationData, 
            Transform transform, 
            SignalBus signalBus, 
            Rigidbody2D rb,
            [Inject(Id = "Physic")] TileBase physicTile,
            [Inject(Id = "Background")] TileBase backgroundTile
            )
        {
            _dragAble = dragAble;
            _mover = mover;
            _asteroidTile = asteroidTile;
            _creationData = creationData;
            _transform = transform;
            _signalBus = signalBus;
            _rb = rb;

            _backgroundTile = backgroundTile;
            _physicTile = physicTile;

            _tilemapMain = tilemaps.First(x => x.name == "Main");
            _tilemapPhysic = tilemaps.First(x => x.name == "Physics");
            _tilemapBackground = tilemaps.First(x => x.name == "Background");
            
            
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
                var pos = gridCell.ToVector3Int();
                _tilemapMain.SetTile(pos ,_asteroidTile.Tile);
                _tilemapPhysic.SetTile(pos, _physicTile);
                _tilemapBackground.SetTile(pos, _backgroundTile);
            }
        }

        private async void DragHandler()
        {
            if(_isPlaced) return;
            _rb.bodyType = RigidbodyType2D.Dynamic;
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
                _rb.bodyType = RigidbodyType2D.Static;
                _transform.position = ((Vector2)_transform.position).RoundToInt();
            }
        }

        public void Dispose()
        {
            _dragAble.onDragStart -= DragHandler;
        }
    }
}