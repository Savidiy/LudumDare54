using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroShooter : IShipShooter, IHasTemperature
    {
        private readonly GunBehaviour _gunBehaviour;
        private readonly HeroStats _heroStats;
        private readonly InputProvider _inputProvider;

        private float _cooldownTimer;
        private float _temperature;
        private float _coolingPauseTimer;

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

            if (_coolingPauseTimer > 0)
                _coolingPauseTimer -= deltaTime;
            else if (_temperature > 0)
                _temperature -= _heroStats.TemperatureCoolingPerSecond * deltaTime;
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
            AddShootTemperature();

            _cooldownTimer = _heroStats.ShootCooldown;
            if (IsOverheated())
                _cooldownTimer += _heroStats.OverheatShootCooldownBonus;

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

        private bool IsOverheated()
        {
            return _temperature >= _heroStats.OverheatMaxTemperature;
        }

        private void AddShootTemperature()
        {
            if (_temperature < 0)
                _temperature = 0;

            _temperature += _heroStats.SingleShootTemperature;
            _coolingPauseTimer = _heroStats.AfterShootCoolingPause;

            if (_temperature > _heroStats.OverheatMaxTemperature)
                _temperature = _heroStats.OverheatMaxTemperature;
        }

        public float GetTemperaturePercent()
        {
            return _temperature / _heroStats.OverheatMaxTemperature;
        }
    }
}