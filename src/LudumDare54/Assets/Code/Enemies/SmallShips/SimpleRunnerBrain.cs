using UnityEngine;

namespace LudumDare54
{
    public sealed class SimpleRunnerBrainArgs
    {
        public SimpleRunnerStatsData StatsData { get; }
        public ShipBehaviour ShipBehaviour { get; }

        public SimpleRunnerBrainArgs(SimpleRunnerStatsData statsData, ShipBehaviour shipBehaviour)
        {
            StatsData = statsData;
            ShipBehaviour = shipBehaviour;
        }
    }

    public sealed class SimpleRunnerBrain : IShipMover
    {
        private readonly SimpleRunnerStatsData _stats;
        private readonly ShipBehaviour _shipBehaviour;

        private float _thinkCooldown;
        private float _rotationSpeed;
        private float _isBoosted;

        public SimpleRunnerBrain(SimpleRunnerBrainArgs args)
        {
            _stats = args.StatsData;
            _shipBehaviour = args.ShipBehaviour;
            Think(0);
        }

        private void Think(float deltaTime)
        {
            _thinkCooldown -= deltaTime;
            if (_thinkCooldown > 0f)
                return;

            _thinkCooldown = Random.Range(_stats.MinThinkingCooldown, _stats.MaxThinkingCooldown);
            _rotationSpeed = Random.Range(_stats.MinRotateSpeed, _stats.MaxRotateSpeed);
            _rotationSpeed *= Random.value > 0.5f ? 1f : -1f;
        }

        public void Move(float deltaTime)
        {
            Think(deltaTime);

            float forwardSpeed = _stats.DefaultSpeed;
            float moveShift = forwardSpeed * deltaTime;

            Transform transform = _shipBehaviour.transform;
            transform.position += transform.up * moveShift;

            float rotationSpeed = _rotationSpeed;
            float rotateDelta = rotationSpeed * deltaTime;
            _shipBehaviour.RotateRoot.Rotate(0, 0, rotateDelta);
        }
    }
}