using System;
using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroCameraTracker
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly CameraProvider _cameraProvider;
        private readonly CameraSettings _cameraSettings;
        private IDisposable _updateSubscription;

        public HeroCameraTracker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, CameraProvider cameraProvider,
            CameraSettings cameraSettings)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _cameraProvider = cameraProvider;
            _cameraSettings = cameraSettings;
        }

        public void StartTracking()
        {
            if (_updateSubscription != null)
                return;

            _updateSubscription = _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
            OnUpdate();
        }

        public void StopTracking()
        {
            _updateSubscription?.Dispose();
            _updateSubscription = null;
        }

        private void OnUpdate()
        {
            if (!_heroShipHolder.TryGetHeroShip(out Ship ship))
                return;

            Transform cameraTransform = _cameraProvider.Camera.transform;
            cameraTransform.position = ship.Position + _cameraSettings.HeroOffset;
            cameraTransform.rotation = ship.Rotation;
        }
    }
}