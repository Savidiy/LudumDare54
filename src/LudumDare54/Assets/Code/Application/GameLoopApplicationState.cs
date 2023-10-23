using System.Collections.Generic;
using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class GameLoopApplicationState : IState, IStateWithExit, IApplicationState
    {
        private readonly SoundPlayer _soundPlayer;
        private readonly SoundSettings _soundSettings;
        private readonly List<IActivatable> _activatables = new();
        private readonly Radar _radar;

        public GameLoopApplicationState(HeroCameraTracker heroCameraTracker, ShipMoveInvoker shipMoveInvoker,
            HudWindow hudWindow, Radar radar, BulletMoveInvoker bulletMoveInvoker, ShipShootInvoker shipShootInvoker,
            BulletCleaner bulletCleaner, BulletCollisionChecker bulletCollisionChecker, ShipHealthTicker shipHealthTicker,
            BulletLifeTimeUpdater bulletLifeTimeUpdater, ShipDeathChecker shipDeathChecker, WinLoseChecker winLoseChecker,
            ShipCollisionChecker shipCollisionChecker, InputProvider inputProvider, StarField starField, SoundPlayer soundPlayer,
            SoundSettings soundSettings, EffectUpdater effectUpdater, CoreSystemsActivator coreSystemsActivator)
        {
            _radar = radar;
            _soundPlayer = soundPlayer;
            _soundSettings = soundSettings;

            _activatables.Add(inputProvider);
            _activatables.Add(hudWindow);
            _activatables.Add(shipCollisionChecker);
            _activatables.Add(bulletCollisionChecker);
            _activatables.Add(shipDeathChecker);
            _activatables.Add(winLoseChecker);
            _activatables.Add(coreSystemsActivator);
        }

        public void Enter()
        {
            _soundPlayer.PlayOnce(_soundSettings.StartLevelSoundId);
            _radar.ClearProgress();
            
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