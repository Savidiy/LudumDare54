using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class WinGameApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly ProgressProvider _progressProvider;
        private readonly List<IActivatable> _activatables = new();

        public WinGameApplicationState(WinGameWindow winGameWindow, ProgressProvider progressProvider, HudWindow hudWindow,
            CoreSystemsActivator coreSystemsActivator)
        {
            _progressProvider = progressProvider;

            _activatables.Add(winGameWindow);
            _activatables.Add(hudWindow);
            _activatables.Add(coreSystemsActivator);
        }

        public void Enter()
        {
            for (var index = 0; index < _activatables.Count; index++)
            {
                IActivatable activatable = _activatables[index];
                activatable.Activate();
            }

            _progressProvider.ResetProgress();
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