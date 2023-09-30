using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class LoadingLevelApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly LevelDataProvider _levelDataProvider;
        private readonly HudSwitcher _hudSwitcher;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly ShipFactory _shipFactory;

        public LoadingLevelApplicationState(ApplicationStateMachine applicationStateMachine, LevelDataProvider levelDataProvider,
            HudSwitcher hudSwitcher, HeroShipHolder heroShipHolder, ShipFactory shipFactory)
        {
            _applicationStateMachine = applicationStateMachine;
            _levelDataProvider = levelDataProvider;
            _hudSwitcher = hudSwitcher;
            _heroShipHolder = heroShipHolder;
            _shipFactory = shipFactory;
        }

        public void Enter()
        {
            Ship heroShip = _shipFactory.CreateHeroShip();
            _heroShipHolder.SetHeroShip(heroShip);

            _hudSwitcher.TurnOn();
            _applicationStateMachine.EnterToState<GameLoopApplicationState>();
        }

        public void Exit()
        {
            _hudSwitcher.TurnOff();
        }
    }
}