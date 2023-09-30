using System;

namespace LudumDare54
{
    public sealed class ShipMoveInvoker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly LimitedSpaceChecker _limitedSpaceChecker;
        private IDisposable _updateSubscribe;

        public ShipMoveInvoker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder,
            LimitedSpaceChecker limitedSpaceChecker)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _limitedSpaceChecker = limitedSpaceChecker;
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
            float deltaTime = _eventInvoker.DeltaTime;
            if (_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                Move(heroShip, deltaTime);

            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship ship = _enemiesHolder.Ships[index];
                Move(ship, deltaTime);
            }
            
            _limitedSpaceChecker.CheckShips();
        }

        private void Move(Ship ship, float deltaTime)
        {
            IShipMover mover = ship.ShipMover;
            mover.UpdatePosition(deltaTime);
        }
    }
}