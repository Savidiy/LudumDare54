using System;

namespace LudumDare54
{
    public interface IBullet : ICanShiftPosition, IDisposable
    {
        void UpdatePosition(float deltaTime);
        bool IsAlive { get; }
    }
}