using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class LoseLevelApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly HeroShipHolder _heroShipHolder;
        private readonly ProgressProvider _progressProvider;
        private readonly List<IActivatable> _activatables = new();

        public LoseLevelApplicationState(LoseLevelWindow loseLevelWindow, BulletCollisionChecker bulletCollisionChecker,
            ShipDeathChecker shipDeathChecker, HeroShipHolder heroShipHolder, ShipCollisionChecker shipCollisionChecker,
            ProgressProvider progressProvider, HudWindow hudWindow, CoreSystemsActivator coreSystemsActivator)
        {
            _heroShipHolder = heroShipHolder;
            _progressProvider = progressProvider;

            _activatables.Add(coreSystemsActivator);
            _activatables.Add(loseLevelWindow);
            _activatables.Add(hudWindow);
            _activatables.Add(bulletCollisionChecker);
            _activatables.Add(shipDeathChecker);
            _activatables.Add(shipCollisionChecker);
        }

        public void Enter()
        {
            _heroShipHolder.Clear();
            _progressProvider.Progress.HeroDeathCount++;

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