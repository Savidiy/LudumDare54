using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class WinLevelApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly List<IActivatable> _activatables = new();
        private readonly ProgressProvider _progressProvider;

        public WinLevelApplicationState(WinLevelWindow winLevelWindow, HudWindow hudWindow,
            ProgressProvider progressProvider, CoreSystemsActivator coreSystemsActivator)
        {
            _progressProvider = progressProvider;

            _activatables.Add(hudWindow);
            _activatables.Add(winLevelWindow);
            _activatables.Add(coreSystemsActivator);
        }

        public void Enter()
        {
            _progressProvider.Progress.CurrentLevelIndex++;
            _progressProvider.SaveProgress();

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