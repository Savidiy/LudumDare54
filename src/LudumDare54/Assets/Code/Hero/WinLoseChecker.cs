using System;

namespace LudumDare54
{
    public sealed class WinLoseChecker : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly LevelDataProvider _levelDataProvider;
        private IDisposable _updateSubscribe;

        public WinLoseChecker(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder,
            ApplicationStateMachine applicationStateMachine, LevelDataProvider levelDataProvider)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _applicationStateMachine = applicationStateMachine;
            _levelDataProvider = levelDataProvider;
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
            if (_heroShipHolder.TryGetHeroShip(out Ship heroShip) && heroShip.Health.IsDead)
                GameOver();
            else if (_enemiesHolder.Ships.Count == 0)
                Win();
        }

        private void Win()
        {
            if (_levelDataProvider.HasNextLevel())
                _applicationStateMachine.EnterToState<WinLevelApplicationState>();
            else
                _applicationStateMachine.EnterToState<WinGameApplicationState>();
        }

        private void GameOver()
        {
            _applicationStateMachine.EnterToState<LoseLevelApplicationState>();
        }
    }
}