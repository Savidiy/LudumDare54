using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipFactory
    {
        private readonly InputProvider _inputProvider;
        private readonly HeroSettings _heroSettings;
        private readonly ShipStatStaticDataLibrary _shipStatStaticDataLibrary;
        private readonly ShipStaticDataLibrary _shipStaticDataLibrary;
        private readonly HighlightSettings _highlightSettings;

        public ShipFactory(InputProvider inputProvider, HeroSettings heroSettings, HighlightSettings highlightSettings,
            ShipStatStaticDataLibrary shipStatStaticDataLibrary, ShipStaticDataLibrary shipStaticDataLibrary)
        {
            _inputProvider = inputProvider;
            _heroSettings = heroSettings;
            _highlightSettings = highlightSettings;
            _shipStatStaticDataLibrary = shipStatStaticDataLibrary;
            _shipStaticDataLibrary = shipStaticDataLibrary;
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
            var shipStats = new ShipStats(shipStatStaticData);

            var heroMover = new HeroMover(shipBehaviour, shipStats, _inputProvider);
            var heroShooter = new HeroShooter(shipBehaviour, shipStats, _inputProvider);
            var collider = new SimpleCollider(shipBehaviour);

            return new Ship(shipBehaviour, heroMover, shipStats, heroShooter, collider, shipHighlighter);
        }

        public Ship CreateEnemyShip(SpawnPointStaticData spawnPointStaticData)
        {
            string heroShipId = spawnPointStaticData.EnemyId;
            ShipStaticData shipStaticData = _shipStaticDataLibrary.GetShipStaticData(heroShipId);
            ShipBehaviour shipBehaviourPrefab = shipStaticData.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab);
            shipBehaviour.transform.position = spawnPointStaticData.Position;
            shipBehaviour.transform.rotation = Quaternion.Euler(spawnPointStaticData.Rotation);
            shipBehaviour.name = heroShipId;

            string statId = shipStaticData.StatId;
            ShipStatStaticData shipStatStaticData = _shipStatStaticDataLibrary.Get(statId);
            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipStats = new ShipStats(shipStatStaticData);

            var asteroidMover = new AsteroidMover(shipBehaviour, shipStats);
            var asteroidShooter = new AsteroidShooter();
            var collider = new SimpleCollider(shipBehaviour);

            return new Ship(shipBehaviour, asteroidMover, shipStats, asteroidShooter, collider, shipHighlighter);
        }
    }
}