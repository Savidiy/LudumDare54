using System;

namespace LudumDare54
{
    public sealed class BulletLifeTimeUpdater : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly BulletHolder _bulletHolder;
        private IDisposable _updateSubscribe;

        public BulletLifeTimeUpdater(IEventInvoker eventInvoker, BulletHolder bulletHolder)
        {
            _eventInvoker = eventInvoker;
            _bulletHolder = bulletHolder;
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
        }

        private void OnUpdate()
        {
            float deltaTime = _eventInvoker.DeltaTime;

            for (var index = 0; index < _bulletHolder.Bullets.Count; index++)
            {
                IBullet bullet = _bulletHolder.Bullets[index];
                bullet.UpdateLifetime(deltaTime);
            }
        }
    }
}