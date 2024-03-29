﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LudumDare54
{
    public sealed class Ship : IDisposable, ICanShiftPosition
    {
        private readonly ShipBehaviour _behaviour;
        private readonly Transform _transform;

        public Vector3 Position => _transform.position;
        public Quaternion Rotation => _behaviour.RotateRoot.rotation;
        public IShipMover ShipMover { get; }
        public IShipShooter ShipShooter { get; }
        public IShipHealth Health { get; }
        public IShipCollider ShipCollider { get; }
        public ShipHighlighter ShipHighlighter { get; }
        public IDeathAction DeathAction { get; }
        public ShipSounds ShipSounds { get; }
        public IDeathSetup DeathSetup { get; }

        public Ship(ShipBehaviour behaviour, IShipMover shipMover, IShipShooter shipShooter,
            IShipCollider shipCollider, ShipHighlighter shipHighlighter, IShipHealth health, IDeathAction deathAction,
            ShipSounds shipSounds, IDeathSetup deathSetup)
        {
            ShipMover = shipMover;
            ShipShooter = shipShooter;
            ShipCollider = shipCollider;
            ShipHighlighter = shipHighlighter;
            Health = health;
            DeathAction = deathAction;
            ShipSounds = shipSounds;
            DeathSetup = deathSetup;
            _behaviour = behaviour;
            _transform = _behaviour.transform;
        }

        public void Dispose()
        {
            ShipHighlighter.Dispose();
            Object.Destroy(_behaviour.gameObject);
        }

        public void ShiftPosition(Vector3 shift)
        {
            _transform.position += shift;
        }

        public void SetSunIllumination(float normalizedLightDirection)
        {
            for (var index = 0; index < _behaviour.IlluminatedRenderers.Length; index++)
            {
                IlluminatedRenderer renderer = _behaviour.IlluminatedRenderers[index];
                renderer.SetSprite(normalizedLightDirection);
            }
        }
    }
}