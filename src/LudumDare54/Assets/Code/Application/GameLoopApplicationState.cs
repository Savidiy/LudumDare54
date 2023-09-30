using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class GameLoopApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly List<IActivatable> _activatables = new();

        public GameLoopApplicationState(HeroCameraTracker heroCameraPlayerTracker, ShipMoveInvoker shipMoveInvoker,
            HudSwitcher hudSwitcher, Radar radar)
        {
            _activatables.Add(heroCameraPlayerTracker);
            _activatables.Add(shipMoveInvoker);
            _activatables.Add(hudSwitcher);
            _activatables.Add(radar);
        }

        public void Enter()
        {
            foreach (IActivatable activatable in _activatables)
                activatable.Activate();
        }

        public void Exit()
        {
            foreach (IActivatable activatable in _activatables)
                activatable.Deactivate();
        }
    }
}