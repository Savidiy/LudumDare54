using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class GameLoopApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly HeroCameraTracker _heroCameraPlayerTracker;

        public GameLoopApplicationState(HeroCameraTracker heroCameraPlayerTracker)
        {
            _heroCameraPlayerTracker = heroCameraPlayerTracker;
        }

        public void Enter()
        {
            _heroCameraPlayerTracker.StartTracking();
        }

        public void Exit()
        {
            _heroCameraPlayerTracker.StopTracking();
        }
    }
}