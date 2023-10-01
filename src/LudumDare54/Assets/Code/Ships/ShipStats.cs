namespace LudumDare54
{
    public sealed class ShipStats : IShipStats
    {
        private readonly ShipStatStaticData _shipStatStaticData;
        private int _health;

        public float ShootCooldown => _shipStatStaticData.ShootCooldown;
        public int Damage => _shipStatStaticData.ShootDamage;
        public float RotationSpeed => _shipStatStaticData.RotationSpeed;
        public float ForwardSpeed => _shipStatStaticData.ForwardSpeed;
        public float BackwardSpeed => _shipStatStaticData.BackwardSpeed;
        public float StrafeSpeed => _shipStatStaticData.StrafeSpeed;
        public int Health => _health;
        bool IsAlive => _health > 0;
        bool IsDead => !IsAlive;

        public ShipStats(ShipStatStaticData shipStatStaticData)
        {
            _shipStatStaticData = shipStatStaticData;
            _health = _shipStatStaticData.StartHealth;
        }

        public void TakeDamage(IShipDamage damage)
        {
            _health -= damage.Damage;
        }
    }
}