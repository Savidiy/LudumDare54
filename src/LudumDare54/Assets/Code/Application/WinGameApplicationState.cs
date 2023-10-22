using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class WinGameApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly ProgressProvider _progressProvider;
        private readonly List<IActivatable> _activatables = new();

        public WinGameApplicationState(WinGameWindow winGameWindow, ProgressProvider progressProvider,
            HeroCameraTracker heroCameraPlayerTracker, ShipMoveInvoker shipMoveInvoker, HudWindow hudWindow,
            Radar radar, BulletMoveInvoker bulletMoveInvoker, BulletCleaner bulletCleaner,
            ShipHealthTicker shipHealthTicker, BulletLifeTimeUpdater bulletLifeTimeUpdater, StarField starField,
            EffectUpdater effectUpdater)
        {
            _progressProvider = progressProvider;

            _activatables.Add(winGameWindow);
            _activatables.Add(hudWindow);
            _activatables.Add(heroCameraPlayerTracker);
            _activatables.Add(shipMoveInvoker);
            _activatables.Add(radar);
            _activatables.Add(bulletMoveInvoker);
            _activatables.Add(bulletLifeTimeUpdater);
            _activatables.Add(bulletCleaner);
            _activatables.Add(shipHealthTicker);
            _activatables.Add(starField);
            _activatables.Add(effectUpdater);
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