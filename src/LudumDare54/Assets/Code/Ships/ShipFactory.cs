using UnityEngine;
using Zenject;

namespace LudumDare54
{
    public sealed class ShipFactory
    {
        private readonly InputProvider _inputProvider;
        private readonly HeroSettings _heroSettings;
        private readonly ShipStatStaticDataLibrary _shipStatStaticDataLibrary;
        private readonly ShipStaticDataLibrary _shipStaticDataLibrary;
        private readonly IInstantiator _instantiator;
        private readonly HighlightSettings _highlightSettings;

        public ShipFactory(InputProvider inputProvider, HeroSettings heroSettings, HighlightSettings highlightSettings,
            ShipStatStaticDataLibrary shipStatStaticDataLibrary, ShipStaticDataLibrary shipStaticDataLibrary,
            IInstantiator instantiator)
        {
            _inputProvider = inputProvider;
            _heroSettings = heroSettings;
            _highlightSettings = highlightSettings;
            _shipStatStaticDataLibrary = shipStatStaticDataLibrary;
            _shipStaticDataLibrary = shipStaticDataLibrary;
            _instantiator = instantiator;
        }

        public Ship CreateHeroShip()
        {
            string heroShipId = _heroSettings.HeroShipId;
            ShipStaticData shipStaticData = _shipStaticDataLibrary.GetShipStaticData(heroShipId);
            ShipBehaviour shipBehaviourPrefab = shipStaticData.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab);
            shipBehaviour.name = heroShipId;

            string statId = shipStaticData.StatId;
            ShipStatStaticData shipStatStaticData = _shipStatStaticDataLibrary.Get(statId);
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new ShipHealth(shipStatStaticData.StartHealth);

            var heroStats = new HeroStats(shipStatStaticData);
            var heroMover = new HeroMover(shipBehaviour, heroStats, _inputProvider);
            var heroShooter = new HeroShooter(shipBehaviour, heroStats, _inputProvider);
            var collider = new SimpleCollider(shipBehaviour);
            var nullDeathAction = new NullDeathAction();

            return new Ship(shipBehaviour, heroMover, heroShooter, collider, shipHighlighter, shipHealth, nullDeathAction);
        }

        public Ship CreateEnemyShip(SpawnPointStaticData spawnPointStaticData)
        {
            string shipId = spawnPointStaticData.EnemyId;
            Vector3 position = spawnPointStaticData.Position;
            Vector3 rotation = spawnPointStaticData.Rotation;
            return CreateEnemyShip(shipId, position, rotation);
        }

        public Ship CreateEnemyShip(string shipId, Vector3 position, Vector3 rotation)
        {
            ShipStaticData shipStaticData = _shipStaticDataLibrary.GetShipStaticData(shipId);
            ShipBehaviour shipBehaviourPrefab = shipStaticData.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab);
            shipBehaviour.transform.position = position;
            shipBehaviour.transform.rotation = Quaternion.Euler(rotation);
            shipBehaviour.name = shipId;

            string statId = shipStaticData.StatId;
            ShipStatStaticData shipStatStaticData = _shipStatStaticDataLibrary.Get(statId);
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new ShipHealth(shipStatStaticData.StartHealth);

            var asteroidStats = new AsteroidStats(shipStatStaticData);
            var asteroidMover = new AsteroidMover(shipBehaviour, asteroidStats);
            var nullShooter = new NullShooter();
            var collider = new SimpleCollider(shipBehaviour);
            IDeathAction deathAction = CreateDeathAction(shipBehaviour, shipStatStaticData, shipHealth);

            return new Ship(shipBehaviour, asteroidMover, nullShooter, collider, shipHighlighter, shipHealth, deathAction);
        }

        private IDeathAction CreateDeathAction(ShipBehaviour shipBehaviour, ShipStatStaticData shipStatStaticData,
            ShipHealth shipHealth)
        {
            DeathActionData deathActionData = shipStatStaticData.DeathActionData;
            var deathSpawnAction =
                _instantiator.Instantiate<DeathSpawnAction>(new object[] {deathActionData, shipBehaviour, shipHealth});

            return deathSpawnAction;
        }
    }
}