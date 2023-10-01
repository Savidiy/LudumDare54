using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class TurretBrain : IShipShooter, IShipMover
    {
        private readonly TurretSettings _turretSettings;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly GunBehaviour _gunBehaviour;

        private float _shotCooldown;
        private int _burstIndex;

        public TurretBrain(TurretSettings turretSettings, HeroShipHolder heroShipHolder, GunBehaviour gunBehaviour)
        {
            _turretSettings = turretSettings;
            _heroShipHolder = heroShipHolder;
            _gunBehaviour = gunBehaviour;
        }

        public void UpdateTimer(float deltaTime)
        {
            _shotCooldown -= deltaTime;
        }

        public bool IsWantShoot()
        {
            return _shotCooldown < 0f;
        }

        public void Shoot(List<BulletData> bulletDataBuffer)
        {
            for (var index = 0; index < _gunBehaviour.GunPoints.Length; index++)
            {
                Transform gunPoint = _gunBehaviour.GunPoints[index];
                if (!gunPoint.gameObject.activeSelf)
                    continue;

                Vector3 gunPosition = gunPoint.position;
                Quaternion rotation = gunPoint.rotation;
                var damage = new SimpleDamage(_turretSettings.BulletDamage);
                bulletDataBuffer.Add(new BulletData(gunPosition, rotation, damage));
            }

            _burstIndex++;
            if (_burstIndex >= _turretSettings.BurstSize)
            {
                _burstIndex = 0;
                _shotCooldown = _turretSettings.BurstCooldown;
            }
            else
            {
                _shotCooldown = _turretSettings.SingleShootCooldown;
            }
        }

        public void Move(float deltaTime)
        {
        }
    }
}