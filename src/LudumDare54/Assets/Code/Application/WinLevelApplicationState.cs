using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class WinLevelApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly List<IActivatable> _activatables = new();
        private readonly ProgressProvider _progressProvider;

        public WinLevelApplicationState(HeroCameraTracker heroCameraPlayerTracker, ShipMoveInvoker shipMoveInvoker,
            WinLevelWindow winLevelWindow, Radar radar, BulletMoveInvoker bulletMoveInvoker, HudWindow hudWindow,
            BulletCleaner bulletCleaner, ShipHealthTicker shipHealthTicker, ProgressProvider progressProvider,
            BulletLifeTimeUpdater bulletLifeTimeUpdater, StarField starField)
        {
            _progressProvider = progressProvider;
            
            _activatables.Add(hudWindow);
            _activatables.Add(winLevelWindow);
            _activatables.Add(heroCameraPlayerTracker);
            _activatables.Add(shipMoveInvoker);
            _activatables.Add(radar);
            _activatables.Add(bulletMoveInvoker);
            _activatables.Add(bulletLifeTimeUpdater);
            _activatables.Add(bulletCleaner);
            _activatables.Add(shipHealthTicker);
            _activatables.Add(starField);
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