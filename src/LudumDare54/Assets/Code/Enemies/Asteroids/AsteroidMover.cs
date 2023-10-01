using UnityEngine;

namespace LudumDare54
{
    public sealed class AsteroidMover : IShipMover
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly AsteroidStatsStaticData _stats;
        private readonly bool _isClockwise;

        public AsteroidMover(ShipBehaviour shipBehaviour, AsteroidStatsStaticData stats)
        {
            _stats = stats;
            _shipBehaviour = shipBehaviour;
            _shipBehaviour.RotateRoot.Rotate(0, 0, Random.Range(0, 360));
            _isClockwise = Random.Range(0, 2) == 0;
        }

        public void Move(float deltaTime)
        {
            float forwardSpeed = _stats.ForwardSpeed;
            float moveShift = forwardSpeed * deltaTime;

            Transform transform = _shipBehaviour.transform;
            transform.position += transform.up * moveShift;

            float rotationSpeed = _stats.RotationSpeed;
            float rotateDelta = rotationSpeed * deltaTime;
            float zAngle = _isClockwise ? rotateDelta : -rotateDelta;
            _shipBehaviour.RotateRoot.Rotate(0, 0, zAngle);
        }
    }
}