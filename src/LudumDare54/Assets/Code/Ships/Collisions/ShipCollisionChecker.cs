using System;
using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipCollisionChecker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly ProgressProvider _progressProvider;
        private readonly SoundPlayer _soundPlayer;
        private readonly SoundSettings _soundSettings;
        private IDisposable _updateSubscribe;

        public ShipCollisionChecker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder,
            ProgressProvider progressProvider, SoundPlayer soundPlayer, SoundSettings soundSettings)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _progressProvider = progressProvider;
            _soundPlayer = soundPlayer;
            _soundSettings = soundSettings;
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
            if (!_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                return;

            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship ship = _enemiesHolder.Ships[index];
                if (heroShip.Health is ICanHasInvulnerability {IsInvulnerable: true})
                    return;

                if (HasCollision(heroShip, ship))
                {
                    _soundPlayer.PlayOnce(_soundSettings.HeroRammingSoundId);
                    _progressProvider.Progress.BumperHitCount++;
                    TakeCollisionDamage(heroShip, ship);
                    TakeCollisionDamage(ship, heroShip);
                }
            }
        }

        private static void TakeCollisionDamage(Ship defender, Ship attacker)
        {
            Vector3 attackVector = attacker.Position - defender.Position;
            IShipDamage damage = defender.Health.SelfDamageFromCollision;
            defender.Health.TakeDamage(damage, attackVector);
            defender.ShipHighlighter.Flash();
        }

        private static bool HasCollision(Ship shipA, Ship shipB)
        {
            return shipA.ShipCollider.Collider.HasCollisionWith(shipB.ShipCollider.Collider);
        }
    }
}