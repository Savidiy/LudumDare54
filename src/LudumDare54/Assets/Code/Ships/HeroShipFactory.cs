using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroShipFactory : IConcreteShipFactory
    {
        private readonly InputProvider _inputProvider;
        private readonly HeroSettings _heroSettings;
        private readonly HighlightSettings _highlightSettings;

        public ShipType[] ShipTypes { get; } = {ShipType.Hero};

        public HeroShipFactory(InputProvider inputProvider, HeroSettings heroSettings, HighlightSettings highlightSettings)
        {
            _inputProvider = inputProvider;
            _heroSettings = heroSettings;
            _highlightSettings = highlightSettings;
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            ShipBehaviour shipBehaviourPrefab = _heroSettings.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab, position, rotation);
            shipBehaviour.name = "Hero";

            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new HeroHealth(shipBehaviour, _heroSettings);

            var heroStats = new HeroStats(_heroSettings);
            var heroMover = new HeroMover(shipBehaviour, heroStats, _inputProvider);
            var heroShooter = new HeroShooter(shipBehaviour.GunBehaviour, heroStats, _inputProvider);
            var collider = new SimpleCollider(shipBehaviour);
            var nullDeathAction = new NullDeathAction();

            return new Ship(shipBehaviour, heroMover, heroShooter, collider, shipHighlighter, shipHealth, nullDeathAction);
        }
    }
}