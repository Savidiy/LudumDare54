using UnityEngine;
using Zenject;

namespace LudumDare54
{
    public sealed class HeroShipFactory : IConcreteShipFactory
    {
        private readonly InputProvider _inputProvider;
        private readonly HeroSettings _heroSettings;
        private readonly HighlightSettings _highlightSettings;
        private readonly ProgressSettings _progressSettings;
        private readonly IInstantiator _instantiator;
        private readonly SoundSettings _soundSettings;

        public ShipType[] ShipTypes { get; } = {ShipType.Hero};

        public HeroShipFactory(InputProvider inputProvider, HeroSettings heroSettings, HighlightSettings highlightSettings,
            ProgressSettings progressSettings, IInstantiator instantiator, SoundSettings soundSettings)
        {
            _inputProvider = inputProvider;
            _heroSettings = heroSettings;
            _highlightSettings = highlightSettings;
            _progressSettings = progressSettings;
            _instantiator = instantiator;
            _soundSettings = soundSettings;
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            ShipBehaviour shipBehaviourPrefab = _heroSettings.ShipBehaviourPrefab;
            ShipBehaviour shipBehaviour = Object.Instantiate(shipBehaviourPrefab, position, rotation);
            shipBehaviour.name = "Hero";

            var shipHighlighter = new ShipHighlighter(shipBehaviour.ShipHighlighter, _highlightSettings);
            var shipHealth = new HeroHealth(shipBehaviour, _heroSettings, _progressSettings);

            var heroStats = new HeroStats(_heroSettings);
            var heroMover = new HeroMover(shipBehaviour, heroStats, _inputProvider);
            var heroShooterArgs = new HeroShooterArgs(shipBehaviour.GunBehaviour, heroStats);
            var heroShooter = _instantiator.Instantiate<HeroShooter>(new[] {heroShooterArgs});
            var collider = new SimpleCollider(shipBehaviour);
            var nullDeathAction = new NullDeathAction();
            var shipSounds = new ShipSounds(_soundSettings.HeroShootSoundId, _soundSettings.HeroHurtSoundId);
            var simpleDeathSetup = new SimpleDeathSetup(shipBehaviour.transform, _heroSettings.DeathExplosionType);

            return new Ship(shipBehaviour, heroMover, heroShooter, collider, shipHighlighter, shipHealth, nullDeathAction,
                shipSounds, simpleDeathSetup);
        }
    }
}