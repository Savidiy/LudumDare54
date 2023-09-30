using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LudumDare54
{
    public sealed class Ship : IDisposable
    {
        private readonly ShipBehaviour _behaviour;
        private readonly Transform _transform;

        public Vector3 Position => _transform.position;
        public Quaternion Rotation => _transform.rotation;
        public IShipMover ShipMover { get; }
        public IShipStats Stats { get; }

        public Ship(ShipBehaviour behaviour, IShipMover shipMover, IShipStats stats)
        {
            ShipMover = shipMover;
            Stats = stats;
            _behaviour = behaviour;
            _transform = _behaviour.transform;
        }

        public void Dispose()
        {
            Object.Destroy(_behaviour.gameObject);
        }

        public void ShiftPosition(Vector3 shift)
        {
            _transform.position += shift;
        }
    }
}