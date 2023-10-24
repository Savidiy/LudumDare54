using System;
using UnityEngine;

namespace LudumDare54
{
    public sealed class BulletCollisionChecker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly BulletHolder _bulletHolder;
        private readonly ProgressProvider _progressProvider;
        private readonly SoundPlayer _soundPlayer;
        private readonly ShipDamageExecutor _shipDamageExecutor;
        private readonly EffectStarter _effectStarter;
        private IDisposable _updateSubscribe;

        public BulletCollisionChecker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder,
            BulletHolder bulletHolder, ProgressProvider progressProvider, SoundPlayer soundPlayer,
            ShipDamageExecutor shipDamageExecutor, EffectStarter effectStarter)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _bulletHolder = bulletHolder;
            _progressProvider = progressProvider;
            _soundPlayer = soundPlayer;
            _shipDamageExecutor = shipDamageExecutor;
            _effectStarter = effectStarter;
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
            for (var index = _bulletHolder.Bullets.Count - 1; index >= 0; index--)
            {
                IBullet bullet = _bulletHolder.Bullets[index];
                if (bullet.IsHeroBullet && HasCollisionWithEnemy(bullet, out Ship enemy))
                {
                    _progressProvider.Progress.BulletHitCount++;
                    TakeDamage(enemy, bullet);
                    _bulletHolder.RemoveAt(index);
                }
                else if (!bullet.IsHeroBullet && HasCollisionWithHero(bullet, out Ship hero))
                {
                    TakeDamage(hero, bullet);
                    _bulletHolder.RemoveAt(index);
                }
            }
        }

        private void TakeDamage(Ship ship, IBullet bullet)
        {
            Vector3 attackVector = bullet.Position - ship.Position;
            IShipDamage bulletDamage = bullet.Damage;
            _shipDamageExecutor.MakeDamage(ship, bulletDamage, attackVector);

            if (ship.Health.IsAlive)
                _effectStarter.ShowEffect(EffectType.SmallExplosion, bullet.Position);
        }

        private bool HasCollisionWithHero(IBullet bullet, out Ship ship)
        {
            ship = null;
            if (!_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                return false;

            if (heroShip.Health is ICanHasInvulnerability {IsInvulnerable: true})
                return false;

            ship = heroShip;
            return HasCollision(bullet, heroShip);
        }

        private static bool HasCollision(IBullet bullet, Ship ship)
        {
            return bullet.Collider.HasCollisionWith(ship.ShipCollider.Collider);
        }

        private bool HasCollisionWithEnemy(IBullet bullet, out Ship target)
        {
            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship ship = _enemiesHolder.Ships[index];
                if (HasCollision(bullet, ship))
                {
                    target = ship;
                    return true;
                }
            }

            target = null;
            return false;
        }
    }
}