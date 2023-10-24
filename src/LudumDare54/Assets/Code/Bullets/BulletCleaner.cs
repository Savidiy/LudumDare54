using System;
using UnityEngine;

namespace LudumDare54
{
    public sealed class BulletCleaner : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly BulletHolder _bulletHolder;
        private readonly CameraProvider _cameraProvider;
        private readonly HeroSettings _heroSettings;

        private IDisposable _updateSubscribe;
        private float _minScreenX;
        private float _maxScreenX;
        private float _minScreenY;
        private float _maxScreenY;

        public BulletCleaner(IEventInvoker eventInvoker, BulletHolder bulletHolder, CameraProvider cameraProvider,
            HeroSettings heroSettings)
        {
            _eventInvoker = eventInvoker;
            _bulletHolder = bulletHolder;
            _cameraProvider = cameraProvider;
            _heroSettings = heroSettings;
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
            _minScreenX = -_heroSettings.BulletClearOffset;
            _maxScreenX = Screen.width + _heroSettings.BulletClearOffset;
            _minScreenY = -_heroSettings.BulletClearOffset;
            _maxScreenY = Screen.height + _heroSettings.BulletClearOffset;
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
        }

        private void OnUpdate()
        {
            for (int index = _bulletHolder.Bullets.Count - 1; index >= 0; index--)
            {
                IBullet bullet = _bulletHolder.Bullets[index];

                if (bullet.IsHeroBullet)
                {
                    if (IsOutScreen(bullet))
                        _bulletHolder.RemoveAt(index);
                }
                else if (!bullet.IsAlive)
                {
                    _bulletHolder.RemoveAt(index);
                }
            }
        }

        private bool IsOutScreen(IBullet bullet)
        {
            Vector3 bulletPosition = bullet.Position;
            Vector3 screenPosition = _cameraProvider.Camera.WorldToScreenPoint(bulletPosition);
            return screenPosition.x < _minScreenX || screenPosition.x > _maxScreenX || screenPosition.y < _minScreenY ||
                   screenPosition.y > _maxScreenY;
        }
    }
}