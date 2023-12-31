﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static LudumDare54.ShipType;

namespace LudumDare54
{
    public sealed class AsteroidFactory : IConcreteShipFactory
    {
        private readonly AsteroidLibrary _asteroidLibrary;
        private readonly IInstantiator _instantiator;
        private readonly HighlightSettings _highlightSettings;

        public ShipType[] ShipTypes { get; } =
            {BigAsteroid1, MiddleAsteroid1, MiddleAsteroid2, SmallAsteroid1, SmallAsteroid2, SmallAsteroid3, SmallAsteroid4};

        public AsteroidFactory(HighlightSettings highlightSettings, AsteroidLibrary asteroidLibrary, IInstantiator instantiator)
        {
            _highlightSettings = highlightSettings;
            _asteroidLibrary = asteroidLibrary;
            _instantiator = instantiator;
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            AsteroidStaticData asteroidStaticData = _asteroidLibrary.Get(shipType);
            ShipBehaviour shipBehaviourPrefab = asteroidStaticData.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab, position, rotation);
            shipBehaviour.name = shipType.ToStringCached();

            AsteroidStatsStaticData stats = asteroidStaticData.Stats;
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new ShipHealth(stats.StartHealth, stats.SelfDamageFromCollision);

            var asteroidMover = new AsteroidMover(shipBehaviour, stats);
            var nullShooter = new NullShooter();
            var collider = new SimpleCollider(shipBehaviour);
            IDeathAction deathAction = CreateDeathAction(shipBehaviour, stats, shipHealth);
            var shipSounds = new ShipSounds(SoundIdData.Empty, stats.HurtSoundId);
            var simpleDeathSetup = new SimpleDeathSetup(shipBehaviour.transform, stats.DeathExplosionType);

            return new Ship(shipBehaviour, asteroidMover, nullShooter, collider, shipHighlighter, shipHealth, deathAction,
                shipSounds, simpleDeathSetup);
        }

        private IDeathAction CreateDeathAction(ShipBehaviour shipBehaviour, AsteroidStatsStaticData stats,
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