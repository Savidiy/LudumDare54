using System;

namespace LudumDare54
{
    public sealed class BulletMoveInvoker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly BulletHolder _bulletHolder;
        private readonly LimitedSpaceChecker _limitedSpaceChecker;
        private IDisposable _updateSubscribe;

        public BulletMoveInvoker(IEventInvoker eventInvoker, BulletHolder bulletHolder, LimitedSpaceChecker limitedSpaceChecker)
        {
            _eventInvoker = eventInvoker;
            _bulletHolder = bulletHolder;
            _limitedSpaceChecker = limitedSpaceChecker;
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
                Move(bullet, deltaTime);
            }
            
            _limitedSpaceChecker.CheckBullets();
        }

        private void Move(IBullet bullet, float deltaTime)
        {
            bullet.UpdatePosition(deltaTime);
        }
    }
}