using UnityEngine;
using Zenject;
using static LudumDare54.ShipType;

namespace LudumDare54
{
    public sealed class SmallShipsFactory : IConcreteShipFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly SmallShipsSettings _smallShipsSettings;
        private readonly HighlightSettings _highlightSettings;

        public ShipType[] ShipTypes { get; } =
            {StupidCircleDude};

        public SmallShipsFactory(HighlightSettings highlightSettings, IInstantiator instantiator,
            SmallShipsSettings smallShipsSettings)
        {
            _highlightSettings = highlightSettings;
            _instantiator = instantiator;
            _smallShipsSettings = smallShipsSettings;
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            StupidCircleDudeData data = _smallShipsSettings.StupidCircleDudeData;
            ShipBehaviour shipBehaviourPrefab = data.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab, position, rotation);
            shipBehaviour.name = shipType.ToStringCached();

            StupidCircleDudeStatsData statsData = data.Stats;
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new ShipHealth(statsData.StartHealth, statsData.SelfDamageFromCollision);

            var brainArgs = new StupidCircleDudeBrainArgs(statsData, shipBehaviour);
            var brain = _instantiator.Instantiate<StupidCircleDudeBrain>(new object[] {brainArgs});

            var collider = new SimpleCollider(shipBehaviour);
            IDeathAction deathAction = new NullDeathAction();
            var shipSounds = new ShipSounds(statsData.ShootSoundId, statsData.HurtSoundId);
            var simpleDeathSetup = new SimpleDeathSetup(shipBehaviour.transform, statsData.DeathExplosionType);

            return new Ship(shipBehaviour, brain, brain, collider, shipHighlighter, shipHealth, deathAction,
                shipSounds, simpleDeathSetup);
        }
    }
}