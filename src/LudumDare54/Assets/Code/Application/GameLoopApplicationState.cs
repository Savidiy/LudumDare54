using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class GameLoopApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly HeroCameraTracker _heroCameraPlayerTracker;
        private readonly PlayerInputShipControls _playerInputShipControls;
        private readonly ShipMoving _shipMoving;

        public GameLoopApplicationState(HeroCameraTracker heroCameraPlayerTracker, PlayerInputShipControls playerInputShipControls,
            ShipMoving shipMoving)
        {
            _heroCameraPlayerTracker = heroCameraPlayerTracker;
            _playerInputShipControls = playerInputShipControls;
            _shipMoving = shipMoving;
        }

        public void Enter()
        {
            _playerInputShipControls.Activate();
            _shipMoving.Activate();
            _heroCameraPlayerTracker.Activate();
        }

        public void Exit()
        {
            _playerInputShipControls.Deactivate();
            _shipMoving.Deactivate();
            _heroCameraPlayerTracker.Deactivate();
        }
    }
}