using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipFactory
    {
        private const string HERO_SHIP_ASSET = "HeroShip";
        private readonly AssetProvider _assetProvider;

        public ShipFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public Ship CreateHeroShip()
        {
            if (!_assetProvider.TryGet(HERO_SHIP_ASSET, out ShipBehaviour shipPrefab))
                throw new System.Exception($"'{HERO_SHIP_ASSET}' asset not found");

            ShipBehaviour shipBehaviour = Object.Instantiate(shipPrefab);
            shipBehaviour.name = HERO_SHIP_ASSET;
            return new Ship(shipBehaviour);
        }
    }
}