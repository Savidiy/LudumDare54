using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class LoseLevelApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly HeroShipHolder _heroShipHolder;
        private readonly ProgressProvider _progressProvider;
        private readonly List<IActivatable> _activatables = new();

        public LoseLevelApplicationState(HeroCameraTracker heroCameraPlayerTracker, ShipMoveInvoker shipMoveInvoker,
            LoseLevelWindow loseLevelWindow, Radar radar, BulletMoveInvoker bulletMoveInvoker, ShipShootInvoker shipShootInvoker,
            BulletCleaner bulletCleaner, BulletCollisionChecker bulletCollisionChecker, ShipHealthTicker shipHealthTicker,
            BulletLifeTimeUpdater bulletLifeTimeUpdater, ShipDeathChecker shipDeathChecker, HeroShipHolder heroShipHolder,
            ShipCollisionChecker shipCollisionChecker, StarField starField, ProgressProvider progressProvider, HudWindow hudWindow,
            EffectUpdater effectUpdater)
        {
            _heroShipHolder = heroShipHolder;
            _progressProvider = progressProvider;

            _activatables.Add(loseLevelWindow);
            _activatables.Add(hudWindow);
            _activatables.Add(heroCameraPlayerTracker);
            _activatables.Add(shipMoveInvoker);
            _activatables.Add(radar);
            _activatables.Add(shipShootInvoker);
            _activatables.Add(shipCollisionChecker);
            _activatables.Add(bulletMoveInvoker);
            _activatables.Add(bulletCollisionChecker);
            _activatables.Add(bulletLifeTimeUpdater);
            _activatables.Add(bulletCleaner);
            _activatables.Add(shipHealthTicker);
            _activatables.Add(shipDeathChecker);
            _activatables.Add(starField);
            _activatables.Add(effectUpdater);
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