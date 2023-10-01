using System;

namespace LudumDare54
{
    public sealed class BulletCleaner : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly BulletHolder _bulletHolder;
        private IDisposable _updateSubscribe;

        public BulletCleaner(IEventInvoker eventInvoker, BulletHolder bulletHolder)
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
            for (int index = _bulletHolder.Bullets.Count - 1; index >= 0; index--)
            {
                IBullet bullet = _bulletHolder.Bullets[index];
                if (!bullet.IsAlive)
                    _bulletHolder.RemoveAt(index);
            }
            
            
        }
    }
}