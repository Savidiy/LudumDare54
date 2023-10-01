using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroShooter : IShipShooter
    {
        private readonly GunBehaviour _gunBehaviour;
        private readonly HeroStats _heroStats;
        private readonly InputProvider _inputProvider;

        private float _cooldownTimer;

        public HeroShooter(GunBehaviour gunBehaviour, HeroStats heroStats, InputProvider inputProvider)
        {
            _gunBehaviour = gunBehaviour;
            _heroStats = heroStats;
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
            _cooldownTimer = _heroStats.ShootCooldown;

            for (var index = 0; index < _gunBehaviour.GunPoints.Length; index++)
            {
                Transform gunPoint = _gunBehaviour.GunPoints[index];
                if (!gunPoint.gameObject.activeSelf)
                    continue;

                Vector3 gunPosition = gunPoint.position;
                Quaternion rotation = gunPoint.rotation;
                var damage = new SimpleDamage(_heroStats.Damage);
                bulletDataBuffer.Add(new BulletData(_heroStats.BulletId, gunPosition, rotation, damage));
            }
        }
    }
}