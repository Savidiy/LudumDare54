using System;
using System.Collections.Generic;

namespace LudumDare54
{
    public sealed class ShipShootInvoker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly BulletFactory _bulletFactory;
        private readonly BulletHolder _bulletHolder;
        private readonly SoundPlayer _soundPlayer;
        private readonly List<BulletData> _bulletDataBuffer = new();
        private IDisposable _updateSubscribe;

        public ShipShootInvoker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder,
            BulletFactory bulletFactory, BulletHolder bulletHolder, SoundPlayer soundPlayer)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _bulletFactory = bulletFactory;
            _bulletHolder = bulletHolder;
            _soundPlayer = soundPlayer;
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
            if (_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                Shoot(heroShip, deltaTime, true);

            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship ship = _enemiesHolder.Ships[index];
                Shoot(ship, deltaTime, false);
            }
        }

        private void Shoot(Ship ship, float deltaTime, bool isHero)
        {
            IShipShooter shooter = ship.ShipShooter;
            shooter.UpdateTimer(deltaTime);
            
            _bulletDataBuffer.Clear();
            if (shooter.IsWantShoot())
            {
                shooter.Shoot(_bulletDataBuffer);
                _soundPlayer.PlayOnce(ship.ShipSounds.ShootSoundId);
            }

            for (var index = 0; index < _bulletDataBuffer.Count; index++)
            {
                IBullet bullet = _bulletFactory.CreateBullet(_bulletDataBuffer[index], isHero);
                _bulletHolder.AddBullet(bullet);
            }
        }
    }
}