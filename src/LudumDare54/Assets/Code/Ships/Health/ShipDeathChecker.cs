using System;

namespace LudumDare54
{
    public sealed class ShipDeathChecker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private IDisposable _updateSubscribe;

        public ShipDeathChecker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder)
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
            for (int index = _enemiesHolder.Ships.Count - 1; index >= 0; index--)
            {
                Ship ship = _enemiesHolder.Ships[index];
                if (ship.Health.IsDead)
                {
                    _enemiesHolder.RemoveAt(index);
                    ship.DeathAction.Invoke();
                }
            }

            if (_heroShipHolder.TryGetHeroShip(out Ship heroShip) && heroShip.Health.IsDead)
                _heroShipHolder.RemoveHeroShip();
        }
    }
}