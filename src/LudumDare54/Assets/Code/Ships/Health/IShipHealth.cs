using UnityEngine;

namespace LudumDare54
{
    public interface IShipHealth
    {
        int Health { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        bool IsDead { get; }
        IShipDamage SelfDamageFromCollision { get; }
        void TakeDamage(IShipDamage damage, Vector3 attackVector);
        void UpdateTimer(float deltaTime);
    }
}