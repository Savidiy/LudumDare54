using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipDamageExecutor
    {
        private readonly EffectStarter _effectStarter;
        private readonly SoundPlayer _soundPlayer;

        public ShipDamageExecutor(EffectStarter effectStarter, SoundPlayer soundPlayer)
        {
            _effectStarter = effectStarter;
            _soundPlayer = soundPlayer;
        }

        public void MakeDamage(Ship ship, IShipDamage damage, Vector3 attackVector)
        {
            ship.Health.TakeDamage(damage, attackVector);
            ship.ShipHighlighter.Flash();
            _soundPlayer.PlayOnce(ship.ShipSounds.HurtSoundId);

            if (ship.Health.IsDead && ship.DeathSetup.HasDeathEffect)
                _effectStarter.ShowEffect(ship.DeathSetup.EffectType, ship.DeathSetup.Position);
        }
    }
}