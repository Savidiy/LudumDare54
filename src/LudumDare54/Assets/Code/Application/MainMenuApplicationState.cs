using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class MainMenuApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly List<IActivatable> _activatables = new();

        public MainMenuApplicationState(MainMenuWindow mainMenuWindow)
        {
            _activatables.Add(mainMenuWindow);
        }

        public void Enter()
        {
            for (var index = 0; index < _activatables.Count; index++)
            {
                IActivatable activatable = _activatables[index];
                activatable.Activate();
            }
        }

        public void Exit()
        {
            for (int index = _activatables.Count - 1; index >= 0; index--)
            {
                IActivatable activatable = _activatables[index];
                activatable.Deactivate();
            }
        }
    }
}