using System.Collections.Generic;

namespace LudumDare54
{
    public interface IShipShooter
    {
        void UpdateTimer(float deltaTime);
        bool IsWantShoot();
        void Shoot(List<BulletData> bulletDataBuffer);
    }
}