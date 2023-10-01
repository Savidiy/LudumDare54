﻿using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class WinGameApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly ProgressProvider _progressProvider;
        private readonly List<IActivatable> _activatables = new();

        public WinGameApplicationState(WinGameWindow winGameWindow, ProgressProvider progressProvider,
            HeroCameraTracker heroCameraPlayerTracker, ShipMoveInvoker shipMoveInvoker,
            Radar radar, BulletMoveInvoker bulletMoveInvoker, BulletCleaner bulletCleaner, 
            ShipHealthTicker shipHealthTicker, BulletLifeTimeUpdater bulletLifeTimeUpdater)
        {
            _progressProvider = progressProvider;
            
            _activatables.Add(winGameWindow);
            _activatables.Add(heroCameraPlayerTracker);
            _activatables.Add(shipMoveInvoker);
            _activatables.Add(radar);
            _activatables.Add(bulletMoveInvoker);
            _activatables.Add(bulletLifeTimeUpdater);
            _activatables.Add(bulletCleaner);
            _activatables.Add(shipHealthTicker);
        }

        public void Enter()
        {
            _progressProvider.ResetProgress();
            
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