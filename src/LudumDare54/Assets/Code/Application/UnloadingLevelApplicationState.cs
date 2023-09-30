using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class UnloadingLevelApplicationState : IState, IApplicationState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly BulletHolder _bulletHolder;

        public UnloadingLevelApplicationState(ApplicationStateMachine applicationStateMachine, HeroShipHolder heroShipHolder,
            EnemiesHolder enemiesHolder, BulletHolder bulletHolder)
        {
            _applicationStateMachine = applicationStateMachine;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _bulletHolder = bulletHolder;
        }

        public void Enter()
        {
            _enemiesHolder.Clear();
            _heroShipHolder.Clear();
            _bulletHolder.Clear();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}