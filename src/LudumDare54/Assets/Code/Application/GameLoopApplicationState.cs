using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class GameLoopApplicationState : IState, IApplicationState
    {
        private readonly TextUpdater _textUpdater;

        public GameLoopApplicationState(TextUpdater textUpdater)
        {
            _textUpdater = textUpdater;
        }

        public void Enter()
        {
            _textUpdater.StartUpdate();
        }
    }
}