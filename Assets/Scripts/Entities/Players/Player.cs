using Signals;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Entities.Players
{
    public class Player: IInitializable, ITickable
    {
        private readonly SignalBus _signalBus;
        private readonly Transform _transform;
        private readonly CameraController _cameraController;
        private Vector2Int _positionInPreviousFrame;

        public Vector2 Position => _transform.position;
        
        public Player(SignalBus signalBus, 
            [Inject(Id = "Player")]Transform transform,
            CameraController cameraController)
        {
            _signalBus = signalBus;
            _transform = transform;
            _cameraController = cameraController;
        }

        public void Initialize()
        {
            _positionInPreviousFrame = ((Vector2)_transform.position).RoundToVector2Int();
        }

        public void Tick()
        {
            MonitorPositionChange();
        }

        private void MonitorPositionChange()
        {
            var currentPosition = ((Vector2)_transform.position).RoundToVector2Int();
            if(currentPosition == _positionInPreviousFrame) return;

            _positionInPreviousFrame = currentPosition;
            _signalBus.Fire(new PlayerMoveSignal()
            {
                Position = currentPosition,
                FoV = new Vector2Int(2, 1) * _cameraController.CameraSize + new Vector2Int(5, 5)
            });
        }
    }
}