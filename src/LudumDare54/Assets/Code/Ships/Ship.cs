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
        public IShipControls ShipControls { get; }
        public IShipStats Stats { get; }

        public Ship(ShipBehaviour behaviour, IShipControls shipControls, IShipStats stats)
        {
            ShipControls = shipControls;
            Stats = stats;
            _behaviour = behaviour;
            _transform = _behaviour.transform;
        }

        public void Dispose()
        {
            Object.Destroy(_behaviour.gameObject);
        }

        public void Rotate(float rotation)
        {
            _transform.Rotate(0, 0, -rotation);
        }

        public void Move(float movement)
        {
            Vector3 shift = _transform.up * movement;
            _transform.position += shift;
        }

        public void Strafe(float strafe)
        {
            Vector3 shift = _transform.right * strafe;
            _transform.position += shift;
        }
    }
}