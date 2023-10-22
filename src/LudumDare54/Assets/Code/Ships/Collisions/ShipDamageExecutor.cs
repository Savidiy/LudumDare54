using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipDamageExecutor
    {
        private readonly EffectStarter _effectStarter;

        public ShipDamageExecutor(EffectStarter effectStarter)
        {
            _effectStarter = effectStarter;
        }
        
        public void MakeDamage(Ship ship, IShipDamage damage, Vector3 attackVector)
        {
            ship.Health.TakeDamage(damage, attackVector);
            ship.ShipHighlighter.Flash();
            if (ship.Health.IsDead && ship.DeathSetup.HasDeathEffect)
                _effectStarter.ShowEffect(ship.DeathSetup.EffectType, ship.DeathSetup.Position);
        }
    }
}