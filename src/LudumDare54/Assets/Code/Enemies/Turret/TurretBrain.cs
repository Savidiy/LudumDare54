using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class TurretBrainArgs
    {
        public TurretStatsData TurretStatsData { get; }
        public ShipBehaviour ShipBehaviour { get; }

        public TurretBrainArgs(TurretStatsData turretStatsData, ShipBehaviour shipBehaviour)
        {
            TurretStatsData = turretStatsData;
            ShipBehaviour = shipBehaviour;
        }
    }

    public sealed class TurretBrain : IShipShooter, IShipMover
    {
        private readonly TurretBrainArgs _args;
        private readonly HeroShipHolder _heroShipHolder;

        private float _shotCooldown;
        private int _burstIndex;
        private TurretState _state = TurretState.FreeRotation;
        private bool _isRightRotation;
        private float _alarmTimer;

        public TurretBrain(TurretBrainArgs args, HeroShipHolder heroShipHolder)
        {
            _heroShipHolder = heroShipHolder;
            _args = args;
            _isRightRotation = Random.value > 0.5f;
            _shotCooldown = Random.value * _args.TurretStatsData.BurstCooldown;
        }

        public void UpdateTimer(float deltaTime)
        {
            _shotCooldown -= deltaTime;
        }

        public bool IsWantShoot()
        {
            if (!_args.TurretStatsData.NeedSeeHeroToShoot)
                return _shotCooldown < 0f;

            bool hasHero = _heroShipHolder.TryGetHeroShip(out Ship heroShip);
            if (_state is TurretState.AimHero or TurretState.FindHero &&
                hasHero && IsHeroInAngle(heroShip))
                return _shotCooldown < 0f;

            return false;
        }

        public void Shoot(List<BulletData> bulletDataBuffer)
        {
            Transform[] gunPoints = _args.ShipBehaviour.GunBehaviour.GunPoints;
            TurretStatsData turretStatsData = _args.TurretStatsData;
            for (var index = 0; index < gunPoints.Length; index++)
            {
                Transform gunPoint = gunPoints[index];
                if (!gunPoint.gameObject.activeSelf)
                    continue;

                string bulletId = turretStatsData.BulletId;
                Vector3 gunPosition = gunPoint.position;
                Quaternion rotation = gunPoint.rotation;
                var damage = new SimpleDamage(turretStatsData.BulletDamage);
                bulletDataBuffer.Add(new BulletData(bulletId, gunPosition, rotation, damage));
            }

            _burstIndex++;
            if (_burstIndex >= turretStatsData.BurstSize)
            {
                _burstIndex = 0;
                _shotCooldown = turretStatsData.BurstCooldown;
            }
            else
            {
                _shotCooldown = turretStatsData.SingleShootCooldown;
            }
        }

        public void Move(float deltaTime)
        {
            bool hasHero = _heroShipHolder.TryGetHeroShip(out Ship heroShip);

            if (!hasHero)
                _state = TurretState.FreeRotation;

            if (_state == TurretState.FreeRotation)
                FreeRotation(deltaTime, hasHero, heroShip);
            else if (_state == TurretState.AimHero && hasHero)
                AimToHero(deltaTime, heroShip);
            else if (_state == TurretState.FindHero && hasHero)
                FindHero(deltaTime, heroShip);
        }

        private void FreeRotation(float deltaTime, bool hasHero, Ship heroShip)
        {
            if (hasHero && IsHeroInDistance(heroShip))
                _state = TurretState.AimHero;

            float rotationSpeed = _args.TurretStatsData.RotationSpeed;
            float angle = _isRightRotation ? rotationSpeed * deltaTime : -rotationSpeed * deltaTime;
            _args.ShipBehaviour.RotateRoot.Rotate(0, 0, angle);
        }

        private void AimToHero(float deltaTime, Ship heroShip)
        {
            if (!IsHeroInDistance(heroShip))
                _state = TurretState.FindHero;

            _alarmTimer = _args.TurretStatsData.AlarmDuration;

            Vector3 heroPosition = heroShip.Position;
            Vector3 turretPosition = _args.ShipBehaviour.RotateRoot.position;
            turretPosition.z = heroPosition.z;

            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, heroPosition - turretPosition);
            Quaternion rootRotation = _args.ShipBehaviour.RotateRoot.rotation;
            float maxDegreesDelta = _args.TurretStatsData.RotationSpeed * deltaTime;
            Quaternion rotation = Quaternion.RotateTowards(rootRotation, lookRotation, maxDegreesDelta);
            _args.ShipBehaviour.RotateRoot.rotation = rotation;
        }

        private void FindHero(float deltaTime, Ship heroShip)
        {
            if (IsHeroInDistance(heroShip))
            {
                _state = TurretState.AimHero;
                return;
            }

            _alarmTimer -= deltaTime;
            if (_alarmTimer > 0)
                return;

            _state = TurretState.FreeRotation;
            _isRightRotation = !_isRightRotation;
        }

        private bool IsHeroInDistance(Ship heroShip)
        {
            float distanceToHero = Vector3.Distance(heroShip.Position, _args.ShipBehaviour.transform.position);
            float detectDistance = _args.TurretStatsData.DetectDistance;
            bool isHeroInDistance = distanceToHero < detectDistance;
            return isHeroInDistance;
        }

        private bool IsHeroInAngle(Ship heroShip)
        {
            if (!_args.TurretStatsData.HasAimAngle)
                return true;

            Vector3 heroPosition = heroShip.Position;
            Vector3 turretPosition = _args.ShipBehaviour.RotateRoot.position;
            turretPosition.z = heroPosition.z;

            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, heroPosition - turretPosition);
            Quaternion rootRotation = _args.ShipBehaviour.RotateRoot.rotation;
            float angle = Quaternion.Angle(rootRotation, lookRotation);
            return angle < _args.TurretStatsData.AimAngleDegree;
        }
    }
}