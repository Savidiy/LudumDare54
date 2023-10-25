using System;
using UnityEngine;
using Zenject;
using static LudumDare54.ShipType;
using Object = UnityEngine.Object;

namespace LudumDare54
{
    public sealed class SmallShipsFactory : IConcreteShipFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly SmallShipsSettings _smallShipsSettings;
        private readonly HighlightSettings _highlightSettings;

        public ShipType[] ShipTypes { get; } =
            {StupidCircleDude, SimpleRunner};

        public SmallShipsFactory(HighlightSettings highlightSettings, IInstantiator instantiator,
            SmallShipsSettings smallShipsSettings)
        {
            _highlightSettings = highlightSettings;
            _instantiator = instantiator;
            _smallShipsSettings = smallShipsSettings;
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            return shipType switch
            {
                StupidCircleDude => CreateStupidCircleDude(shipType, position, rotation),
                SimpleRunner => CreateSimpleRunner(shipType, position, rotation),
                _ => throw new ArgumentOutOfRangeException(nameof(shipType), shipType, null)
            };
        }

        private Ship CreateStupidCircleDude(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            StupidCircleDudeData data = _smallShipsSettings.StupidCircleDudeData;
            ShipBehaviour shipBehaviour = CreateShipBehaviour(shipType, position, rotation, data.ShipBehaviourPrefab);

            var brainArgs = new StupidCircleDudeBrainArgs(data.Stats, shipBehaviour);
            var brain = _instantiator.Instantiate<StupidCircleDudeBrain>(new object[] {brainArgs});

            return CreateShip(shipBehaviour, data.CommonData, brain, brain);
        }

        private Ship CreateSimpleRunner(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            SimpleRunnerData data = _smallShipsSettings.SimpleRunnerData;
            ShipBehaviour shipBehaviour = CreateShipBehaviour(shipType, position, rotation, data.ShipBehaviourPrefab);

            var brainArgs = new SimpleRunnerBrainArgs(data.Stats, shipBehaviour);
            var brain = _instantiator.Instantiate<SimpleRunnerBrain>(new object[] {brainArgs});
            var nullShooter = new NullShooter();

            return CreateShip(shipBehaviour, data.CommonData, brain, nullShooter);
        }

        private Ship CreateShip(ShipBehaviour shipBehaviour, CommonSmallShipData commonData, IShipMover mover,
            IShipShooter shooter)
        {
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new ShipHealth(commonData.StartHealth, commonData.SelfDamageFromCollision);

            var collider = new SimpleCollider(shipBehaviour);
            IDeathAction deathAction = new NullDeathAction();
            var shipSounds = new ShipSounds(commonData.ShootSoundId, commonData.HurtSoundId);
            var simpleDeathSetup = new SimpleDeathSetup(shipBehaviour.transform, commonData.DeathExplosionType);

            return new Ship(shipBehaviour, mover, shooter, collider, shipHighlighter, shipHealth, deathAction,
                shipSounds, simpleDeathSetup);
        }

        private static ShipBehaviour CreateShipBehaviour(ShipType shipType, Vector3 position, Quaternion rotation,
            ShipBehaviour shipBehaviourPrefab)
        {
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab, position, rotation);
            shipBehaviour.name = shipType.ToStringCached();
            return shipBehaviour;
        }
    }
}