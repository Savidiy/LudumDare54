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
        private IDisposable _updateSubscribe;

        public BulletCollisionChecker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder,
            BulletHolder bulletHolder)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
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
            for (var index = _bulletHolder.Bullets.Count - 1; index >= 0; index--)
            {
                IBullet bullet = _bulletHolder.Bullets[index];
                if (bullet.IsHeroBullet && HasCollisionWithEnemy(bullet, out Ship enemy))
                {
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

        private static void TakeDamage(Ship ship, IBullet bullet)
        {
            Vector3 attackVector = bullet.Position - ship.Position;
            ship.Health.TakeDamage(bullet.Damage, attackVector);
            ship.ShipHighlighter.Flash();
        }

        private bool HasCollisionWithHero(IBullet bullet, out Ship ship)
        {
            if (!_heroShipHolder.TryGetHeroShip(out Ship heroShip))
            {
                ship = null;
                return false;
            }

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