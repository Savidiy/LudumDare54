using UnityEngine;

namespace LudumDare54
{
    public interface IShipHealth
    {
        int Health { get; }
        bool IsAlive { get; }
        bool IsDead { get; }
        Vector3 LastAttackVector { get; }
        void TakeDamage(IShipDamage damage, Vector3 attackVector);
    }
}