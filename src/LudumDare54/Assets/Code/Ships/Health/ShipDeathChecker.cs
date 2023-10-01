using System;

namespace LudumDare54
{
    public sealed class ShipDeathChecker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly EnemiesHolder _enemiesHolder;
        private IDisposable _updateSubscribe;

        public ShipDeathChecker(IEventInvoker eventInvoker, EnemiesHolder enemiesHolder)
        {
            _eventInvoker = eventInvoker;
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
        }
    }
}