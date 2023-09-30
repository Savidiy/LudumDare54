using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipFactory
    {
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly HeroSettings _heroSettings;
        private readonly ShipStatStaticDataLibrary _shipStatStaticDataLibrary;
        private readonly ShipStaticDataLibrary _shipStaticDataLibrary;

        public ShipFactory(PlayerInputProvider playerInputProvider, HeroSettings heroSettings,
            ShipStatStaticDataLibrary shipStatStaticDataLibrary, ShipStaticDataLibrary shipStaticDataLibrary)
        {
            _playerInputProvider = playerInputProvider;
            _heroSettings = heroSettings;
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
            var shipStats = new ShipStats(shipStatStaticData);

            var heroMover = new HeroMover(shipBehaviour, shipStats, _playerInputProvider);

            return new Ship(shipBehaviour, heroMover, shipStats);
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
            var shipStats = new ShipStats(shipStatStaticData);

            var asteroidMover = new AsteroidMover(shipBehaviour, shipStats);

            return new Ship(shipBehaviour, asteroidMover, shipStats);
        }
    }
}