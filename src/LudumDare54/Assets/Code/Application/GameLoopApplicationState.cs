using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class GameLoopApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly HeroCameraTracker _heroCameraPlayerTracker;
        private readonly ShipMoveInvoker _shipMoveInvoker;
        private readonly HudSwitcher _hudSwitcher;

        public GameLoopApplicationState(HeroCameraTracker heroCameraPlayerTracker, ShipMoveInvoker shipMoveInvoker,
            HudSwitcher hudSwitcher)
        {
            _heroCameraPlayerTracker = heroCameraPlayerTracker;
            _shipMoveInvoker = shipMoveInvoker;
            _hudSwitcher = hudSwitcher;
        }

        public void Enter()
        {
            _shipMoveInvoker.Activate();
            _heroCameraPlayerTracker.Activate();
            _hudSwitcher.Activate();
        }

        public void Exit()
        {
            _hudSwitcher.Deactivate();
            _shipMoveInvoker.Deactivate();
            _heroCameraPlayerTracker.Deactivate();
        }
    }
}