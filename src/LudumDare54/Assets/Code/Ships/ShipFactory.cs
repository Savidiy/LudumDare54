using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipFactory
    {
        private const string HERO_SHIP_ASSET = "HeroShip";
        private readonly AssetProvider _assetProvider;
        private readonly PlayerInputShipControls _playerInputShipControls;
        private readonly HeroSettings _heroSettings;
        private readonly ShipStaticDataLibrary _shipStaticDataLibrary;

        public ShipFactory(AssetProvider assetProvider, PlayerInputShipControls playerInputShipControls, HeroSettings heroSettings,
            ShipStaticDataLibrary shipStaticDataLibrary)
        {
            _assetProvider = assetProvider;
            _playerInputShipControls = playerInputShipControls;
            _heroSettings = heroSettings;
            _shipStaticDataLibrary = shipStaticDataLibrary;
        }

        public Ship CreateHeroShip()
        {
            if (!_assetProvider.TryGet(HERO_SHIP_ASSET, out ShipBehaviour shipPrefab))
                throw new System.Exception($"'{HERO_SHIP_ASSET}' asset not found");

            ShipBehaviour shipBehaviour = Object.Instantiate(shipPrefab);
            shipBehaviour.name = HERO_SHIP_ASSET;
            string shipStaticDataId = _heroSettings.HeroShipStaticDataId;
            ShipStaticData shipStaticData = _shipStaticDataLibrary.Get(shipStaticDataId);
            var shipStats = new ShipStats(shipStaticData);

            return new Ship(shipBehaviour, _playerInputShipControls, shipStats);
        }
    }
}