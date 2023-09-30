using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LudumDare54
{
    public sealed class Ship : IDisposable
    {
        private readonly ShipBehaviour _behaviour;

        public Vector3 Position => _behaviour.transform.position;
        public Quaternion Rotation => _behaviour.transform.rotation;

        public Ship(ShipBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public void Dispose()
        {
            Object.Destroy(_behaviour.gameObject);
        }
    }
}