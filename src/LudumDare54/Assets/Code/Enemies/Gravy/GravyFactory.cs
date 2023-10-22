using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static LudumDare54.ShipType;

namespace LudumDare54
{
    public sealed class GravyFactory : IConcreteShipFactory
    {
        private readonly GravyLibrary _gravyLibrary;
        private readonly IInstantiator _instantiator;
        private readonly HighlightSettings _highlightSettings;

        public ShipType[] ShipTypes { get; } =
            {BigGravy1, MiddleGravy1, MiddleGravy2, SmallGravy1, SmallGravy2, SmallGravy3,};

        public GravyFactory(HighlightSettings highlightSettings, GravyLibrary gravyLibrary, IInstantiator instantiator)
        {
            _highlightSettings = highlightSettings;
            _gravyLibrary = gravyLibrary;
            _instantiator = instantiator;
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            GravyStaticData gravyStaticData = _gravyLibrary.Get(shipType);
            ShipBehaviour shipBehaviourPrefab = gravyStaticData.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab, position, rotation);
            shipBehaviour.name = shipType.ToStringCached();

            GravyStatsStaticData gravyStatsStaticData = gravyStaticData.Stats;
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new ShipHealth(gravyStatsStaticData.StartHealth, gravyStatsStaticData.SelfDamageFromCollision);

            var gravyMover = new GravyMover(shipBehaviour, gravyStatsStaticData);
            var nullShooter = new NullShooter();
            var collider = new SimpleCollider(shipBehaviour);
            IDeathAction deathAction = CreateDeathAction(shipBehaviour, gravyStatsStaticData, shipHealth);
            var shipSounds = new ShipSounds(SoundIdData.Empty, gravyStatsStaticData.HurtSoundId);
            var simpleDeathSetup = new SimpleDeathSetup(shipBehaviour.RotateRoot, gravyStatsStaticData.DeathExplosionType);

            return new Ship(shipBehaviour, gravyMover, nullShooter, collider, shipHighlighter, shipHealth, deathAction,
                shipSounds, simpleDeathSetup);
        }

        private IDeathAction CreateDeathAction(ShipBehaviour shipBehaviour, GravyStatsStaticData stats,
            ShipHealth shipHealth)
        {
            int minSpawnCount = stats.MinSpawnCount;
            List<DeathSpawnStaticData> spawnStaticDatas = stats.DeathSpawnStaticData;
            var deathSpawnAction = _instantiator.Instantiate<DeathSpawnAction>(
                new object[] {minSpawnCount, spawnStaticDatas, shipBehaviour, shipHealth});

            return deathSpawnAction;
        }
    }
}