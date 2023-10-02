using UnityEngine;

namespace LudumDare54
{
    public sealed class GravyMover : IShipMover
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly GravyStatsStaticData _stats;
        private readonly bool _isClockwiseInsideRotation;
        private readonly bool _isClockwiseOutsideRotation;
        private const float PI2 = 2 * Mathf.PI;

        private float _period;
        private float _outsideDelay;

        public GravyMover(ShipBehaviour shipBehaviour, GravyStatsStaticData stats)
        {
            _stats = stats;
            _shipBehaviour = shipBehaviour;
            _shipBehaviour.RotateRoot.Rotate(0, 0, Random.Range(0, 360));
            _isClockwiseInsideRotation = Random.Range(0, 2) == 0;
            _isClockwiseOutsideRotation = Random.Range(0, 2) == 0;
            _period = Random.Range(0, _stats.OutsideRotationPeriod);
            _outsideDelay = _stats.OutsideRotationDelay;
        }

        public void Move(float deltaTime)
        {
            float forwardSpeed = _stats.ForwardSpeed;
            float moveShift = forwardSpeed * deltaTime;

            Transform transform = _shipBehaviour.transform;
            transform.position += transform.up * moveShift;

            float rotationSpeed = _stats.InsideRotationSpeed;
            float rotateDelta = rotationSpeed * deltaTime;
            float zAngle = _isClockwiseInsideRotation ? rotateDelta : -rotateDelta;
            _shipBehaviour.RotateRoot.Rotate(0, 0, zAngle);

            _period += deltaTime;
            if (_period > _stats.OutsideRotationPeriod)
                _period -= _stats.OutsideRotationPeriod;
            
            if (_outsideDelay > 0)
                _outsideDelay -= deltaTime;

            float sign = _isClockwiseOutsideRotation ? 1 : -1;
            float progress = _period / _stats.OutsideRotationPeriod * PI2 * sign;
            float sin = Mathf.Sin(progress);
            float cos = Mathf.Cos(progress);
            float distance = _stats.OutsideRotationDistance * (1 - _outsideDelay / _stats.OutsideRotationDelay);
            float x = distance * cos;
            float y = distance * sin;
            var position = new Vector3(x, y, 0);
            _shipBehaviour.RotateRoot.localPosition = position;
        }
    }
}