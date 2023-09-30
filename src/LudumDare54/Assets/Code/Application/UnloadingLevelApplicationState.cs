using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class UnloadingLevelApplicationState : IState, IApplicationState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;

        public UnloadingLevelApplicationState(ApplicationStateMachine applicationStateMachine, HeroShipHolder heroShipHolder,
            EnemiesHolder enemiesHolder)
        {
            _applicationStateMachine = applicationStateMachine;
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
        }

        public void Enter()
        {
            _enemiesHolder.Clear();
            _heroShipHolder.Clear();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}