using UnityEngine;
using Zenject;
using static LudumDare54.ShipType;

namespace LudumDare54
{
    public sealed class TurretFactory : IConcreteShipFactory
    {
        private readonly TurretLibrary _turretLibrary;
        private readonly IInstantiator _instantiator;
        private readonly HighlightSettings _highlightSettings;

        public ShipType[] ShipTypes { get; } =
            {SmallTurret};

        public TurretFactory(HighlightSettings highlightSettings, TurretLibrary turretLibrary, IInstantiator instantiator)
        {
            _highlightSettings = highlightSettings;
            _turretLibrary = turretLibrary;
            _instantiator = instantiator;
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            TurretData turretData = _turretLibrary.Get(shipType);
            ShipBehaviour shipBehaviourPrefab = turretData.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab, position, rotation);
            shipBehaviour.name = shipType.ToStringCached();

            TurretStatsData statsData = turretData.Stats;
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new ShipHealth(statsData.StartHealth, statsData.SelfDamageFromCollision);

            GunBehaviour gunBehaviour = shipBehaviour.GunBehaviour;
            var turretBrain = _instantiator.Instantiate<TurretBrain>(new object[] {statsData, gunBehaviour});
            var collider = new SimpleCollider(shipBehaviour);
            IDeathAction deathAction = new NullDeathAction();

            return new Ship(shipBehaviour, turretBrain, turretBrain, collider, shipHighlighter, shipHealth, deathAction);
        }
    }
}