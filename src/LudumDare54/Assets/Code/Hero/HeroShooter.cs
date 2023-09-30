using System.Collections.Generic;

namespace LudumDare54
{
    public sealed class HeroShooter : IShipShooter
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly ShipStats _shipStats;
        private readonly InputProvider _inputProvider;

        private float _cooldownTimer;

        public HeroShooter(ShipBehaviour shipBehaviour, ShipStats shipStats, InputProvider inputProvider)
        {
            _shipBehaviour = shipBehaviour;
            _shipStats = shipStats;
            _inputProvider = inputProvider;
        }

        public void UpdateTimer(float deltaTime)
        {
            if (_cooldownTimer > 0)
                _cooldownTimer -= deltaTime;
        }

        public bool IsWantShoot()
        {
            if (_cooldownTimer > 0)
                return false;
            
            var shootInput = _inputProvider.GetShootInput();
            return shootInput.HasFire1;
        }

        public void Shoot(List<BulletData> bulletDataBuffer)
        {
            _cooldownTimer = _shipStats.ShootCooldown;
            bulletDataBuffer.Add(new BulletData(_shipBehaviour.Gun.position, _shipBehaviour.transform.rotation));
        }
    }
}