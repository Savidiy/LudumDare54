namespace LudumDare54
{
    public sealed class HeroStats
    {
        private readonly ShipStatStaticData _shipStatStaticData;

        public float ShootCooldown => _shipStatStaticData.ShootCooldown;
        public int Damage => _shipStatStaticData.ShootDamage;
        public float RotationSpeed => _shipStatStaticData.RotationSpeed;
        public float ForwardSpeed => _shipStatStaticData.ForwardSpeed;
        public float BackwardSpeed => _shipStatStaticData.BackwardSpeed;
        public float StrafeSpeed => _shipStatStaticData.StrafeSpeed;

        public HeroStats(ShipStatStaticData shipStatStaticData)
        {
            _shipStatStaticData = shipStatStaticData;
        }
    }
}