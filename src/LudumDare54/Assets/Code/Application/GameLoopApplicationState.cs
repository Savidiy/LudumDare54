using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class GameLoopApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly List<IActivatable> _activatables = new();

        public GameLoopApplicationState(HeroCameraTracker heroCameraPlayerTracker, ShipMoveInvoker shipMoveInvoker,
            HudSwitcher hudSwitcher, Radar radar, BulletMoveInvoker bulletMoveInvoker, ShipShootInvoker shipShootInvoker,
            BulletLifetimeKiller bulletLifetimeKiller)
        {
            _activatables.Add(heroCameraPlayerTracker);
            _activatables.Add(shipMoveInvoker);
            _activatables.Add(hudSwitcher);
            _activatables.Add(radar);
            _activatables.Add(shipShootInvoker);
            _activatables.Add(bulletMoveInvoker);
            _activatables.Add(bulletLifetimeKiller);
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