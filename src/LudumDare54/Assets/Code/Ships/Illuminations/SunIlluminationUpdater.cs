using System;

namespace LudumDare54
{
    public sealed class SunIlluminationUpdater : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private IDisposable _updateSubscribe;

        public SunIlluminationUpdater(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
        }

        private void OnUpdate()
        {
            if (_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                SetSunIllumination(heroShip);

            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship ship = _enemiesHolder.Ships[index];
                SetSunIllumination(ship);
            }
        }

        private void SetSunIllumination(Ship ship)
        {
            float eulerAnglesZ = ship.Rotation.eulerAngles.z;
            eulerAnglesZ %= 360f;
            if (eulerAnglesZ < 0f)
                eulerAnglesZ += 360f;

            float normalized = eulerAnglesZ / 360f;

            ship.SetSunIllumination(normalized);
        }
    }
}