using UnityEngine;

namespace LudumDare54
{
    public sealed class AsteroidMover : IShipMover
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly IShipStats _shipStats;

        public AsteroidMover(ShipBehaviour shipBehaviour, IShipStats shipStats)
        {
            _shipStats = shipStats;
            _shipBehaviour = shipBehaviour;
        }

        public void UpdatePosition(float deltaTime)
        {
            float forwardSpeed = _shipStats.ForwardSpeed;
            float moveShift = forwardSpeed * deltaTime;

            Transform transform = _shipBehaviour.transform;
            transform.position += transform.up * moveShift;

            float rotationSpeed = _shipStats.RotationSpeed;
            float rotateDelta = rotationSpeed * deltaTime;
            _shipBehaviour.RotateRoot.Rotate(0, 0, -rotateDelta);
        }
    }
}