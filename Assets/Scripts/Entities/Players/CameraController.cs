using System;
using Installers;
using UnityEngine;
using Zenject;

namespace Entities.Players
{
    public class CameraController: ITickable
    {
        private readonly Transform _targetTransform;
        private readonly Transform _cameraTransform;
        private readonly Camera _camera;
        private readonly PlayerSettings _settings;
        private int _cameraSize = 5;

        public int CameraSize => _cameraSize;

        public CameraController([Inject(Id = "Player")] Transform targetTransform, 
            [Inject(Id = "Camera")] Transform cameraTransform,
            Camera camera, PlayerSettings settings)
        {
            _targetTransform = targetTransform;
            _cameraTransform = cameraTransform;
            _camera = camera;
            _settings = settings;
        }

        public void Tick()
        {
            var targetPos = _targetTransform.position;
            targetPos.z = -10;
            _cameraTransform.position = targetPos;
            var scroll = Input.mouseScrollDelta.y;
            _cameraSize -= Mathf.RoundToInt(scroll);
            _cameraSize = Mathf.Clamp(_cameraSize, _settings.MinCameraSize, _settings.MaxCameraSize);
            _camera.orthographicSize = _cameraSize;
        }
    }
}