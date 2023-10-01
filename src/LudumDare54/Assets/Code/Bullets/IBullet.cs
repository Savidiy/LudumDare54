using System;
using UnityEngine;

namespace LudumDare54
{
    public interface IBullet : ICanShiftPosition, IDisposable
    {
        bool IsAlive { get; }
        bool IsHeroBullet { get; }
        Collider2D Collider { get; }
        IShipDamage Damage { get; }

        void UpdatePosition(float deltaTime);
        void UpdateLifetime(float deltaTime);
    }
}