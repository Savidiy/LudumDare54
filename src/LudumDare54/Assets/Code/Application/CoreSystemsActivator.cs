using System.Collections.Generic;

namespace LudumDare54
{
    public sealed class CoreSystemsActivator : IActivatable
    {
        private readonly List<IActivatable> _activatables = new();

        public CoreSystemsActivator(HeroCameraTracker heroCameraTracker, ShipMoveInvoker shipMoveInvoker, Radar radar,
            BulletMoveInvoker bulletMoveInvoker, ShipShootInvoker shipShootInvoker, BulletCleaner bulletCleaner,
            ShipHealthTicker shipHealthTicker, BulletLifeTimeUpdater bulletLifeTimeUpdater, StarField starField,
            EffectUpdater effectUpdater, SunIlluminationUpdater sunIlluminationUpdater, MainMenuInvoker mainMenuInvoker)
        {
            _activatables.Add(heroCameraTracker);
            _activatables.Add(shipMoveInvoker);
            _activatables.Add(shipShootInvoker);
            _activatables.Add(radar);
            _activatables.Add(bulletMoveInvoker);
            _activatables.Add(bulletLifeTimeUpdater);
            _activatables.Add(bulletCleaner);
            _activatables.Add(shipHealthTicker);
            _activatables.Add(starField);
            _activatables.Add(effectUpdater);
            _activatables.Add(sunIlluminationUpdater);
            _activatables.Add(mainMenuInvoker);
        }

        public void Activate()
        {
            for (var index = 0; index < _activatables.Count; index++)
            {
                IActivatable activatable = _activatables[index];
                activatable.Activate();
            }
        }

        public void Deactivate()
        {
            for (int index = _activatables.Count - 1; index >= 0; index--)
            {
                IActivatable activatable = _activatables[index];
                activatable.Deactivate();
            }
        }
    }
}