using System.Collections.Generic;

namespace LudumDare54
{
    public sealed class AsteroidShooter : IShipShooter
    {
        public void UpdateTimer(float deltaTime)
        {
        }

        public bool IsWantShoot()
        {
            return false;
        }

        public void Shoot(List<BulletData> bulletDataBuffer)
        {
        }
    }
}