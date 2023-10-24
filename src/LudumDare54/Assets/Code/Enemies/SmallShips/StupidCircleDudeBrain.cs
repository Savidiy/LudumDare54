using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class StupidCircleDudeBrainArgs
    {
        public StupidCircleDudeStatsData StupidCircleDudeStatsData { get; }
        public ShipBehaviour ShipBehaviour { get; }

        public StupidCircleDudeBrainArgs(StupidCircleDudeStatsData stupidCircleDudeStatsData, ShipBehaviour shipBehaviour)
        {
            StupidCircleDudeStatsData = stupidCircleDudeStatsData;
            ShipBehaviour = shipBehaviour;
        }
    }

    public sealed class StupidCircleDudeBrain : IShipShooter, IShipMover
    {
        private readonly StupidCircleDudeStatsData _stats;
        private readonly ShipBehaviour _shipBehaviour;

        private float _shootCooldown;
        private float _forwardMoveCooldown;
        private float _thinkCooldown;
        private float _rotationSpeed;

        public StupidCircleDudeBrain(StupidCircleDudeBrainArgs args)
        {
            _stats = args.StupidCircleDudeStatsData;
            _shipBehaviour = args.ShipBehaviour;
            _shootCooldown = Random.value * _stats.MaxShootCooldown;
            _thinkCooldown = Random.value * _stats.MaxThinkingCooldown;
        }

        public void UpdateTimer(float deltaTime)
        {
            _shootCooldown -= deltaTime;
            _thinkCooldown -= deltaTime;
            _forwardMoveCooldown -= deltaTime;
            if (_thinkCooldown > 0f)
                return;

            _thinkCooldown = Random.Range(_stats.MinThinkingCooldown, _stats.MaxThinkingCooldown);
            _rotationSpeed = Random.Range(_stats.MinRotateSpeed, _stats.MaxRotateSpeed);
            _rotationSpeed *= Random.value > 0.5f ? 1f : -1f;
        }

        public bool IsWantShoot()
        {
            return _shootCooldown < 0f;
        }

        public void Shoot(List<BulletData> bulletDataBuffer)
        {
            _shootCooldown = Random.Range(_stats.MinShootCooldown, _stats.MaxShootCooldown);
            _forwardMoveCooldown = _stats.ForwardMoveAfterShootCooldown;

            Transform[] gunPoints = _shipBehaviour.GunBehaviour.GunPoints;
            for (var index = 0; index < gunPoints.Length; index++)
            {
                Transform gunPoint = gunPoints[index];
                if (!gunPoint.gameObject.activeSelf)
                    continue;

                string bulletId = _stats.BulletId;
                Vector3 gunPosition = gunPoint.position;
                Quaternion rotation = gunPoint.rotation;
                var damage = new SimpleDamage(_stats.BulletDamage);
                bulletDataBuffer.Add(new BulletData(bulletId, gunPosition, rotation, damage));
            }
        }

        public void Move(float deltaTime)
        {
            float forwardSpeed = _stats.ForwardSpeed;
            float moveShift = forwardSpeed * deltaTime;

            Transform transform = _shipBehaviour.transform;
            transform.position += transform.up * moveShift;

            if (_forwardMoveCooldown > 0f)
                return;

            float rotationSpeed = _rotationSpeed;
            float rotateDelta = rotationSpeed * deltaTime;
            _shipBehaviour.RotateRoot.Rotate(0, 0, rotateDelta);
        }
    }
}