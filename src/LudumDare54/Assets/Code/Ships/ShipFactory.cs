using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipFactory
    {
        private readonly PlayerInputShipControls _playerInputShipControls;
        private readonly HeroSettings _heroSettings;
        private readonly ShipStatStaticDataLibrary _shipStatStaticDataLibrary;
        private readonly ShipStaticDataLibrary _shipStaticDataLibrary;

        public ShipFactory(PlayerInputShipControls playerInputShipControls, HeroSettings heroSettings,
            ShipStatStaticDataLibrary shipStatStaticDataLibrary, ShipStaticDataLibrary shipStaticDataLibrary)
        {
            _playerInputShipControls = playerInputShipControls;
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

            return new Ship(shipBehaviour, _playerInputShipControls, shipStats);
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

            return new Ship(shipBehaviour, _playerInputShipControls, shipStats);
        }
    }
}