using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroShooter : IShipShooter
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly ShipStats _shipStats;
        private readonly InputProvider _inputProvider;

        private float _cooldownTimer;

        public HeroShooter(ShipBehaviour shipBehaviour, ShipStats shipStats, InputProvider inputProvider)
        {
            _shipBehaviour = shipBehaviour;
            _shipStats = shipStats;
            _inputProvider = inputProvider;
        }

        public void UpdateTimer(float deltaTime)
        {
            if (_cooldownTimer > 0)
                _cooldownTimer -= deltaTime;
        }

        public bool IsWantShoot()
        {
            if (_cooldownTimer > 0)
                return false;

            var shootInput = _inputProvider.GetShootInput();
            return shootInput.HasFire1;
        }

        public void Shoot(List<BulletData> bulletDataBuffer)
        {
            _cooldownTimer = _shipStats.ShootCooldown;
            Vector3 gunPosition = _shipBehaviour.Gun.position;
            Quaternion rotation = _shipBehaviour.transform.rotation;
            var damage = new SimpleDamage(_shipStats.Damage);
            bulletDataBuffer.Add(new BulletData(gunPosition, rotation, damage));
        }
    }
}