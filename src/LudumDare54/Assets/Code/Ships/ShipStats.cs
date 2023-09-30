namespace LudumDare54
{
    public sealed class ShipStats : IShipStats
    {
        private readonly ShipStatStaticData _shipStatStaticData;
        public float ShootCooldown => _shipStatStaticData.ShootCooldown;
        public float RotationSpeed => _shipStatStaticData.RotationSpeed;
        public float ForwardSpeed => _shipStatStaticData.ForwardSpeed;
        public float BackwardSpeed => _shipStatStaticData.BackwardSpeed;
        public float StrafeSpeed => _shipStatStaticData.StrafeSpeed;

        public ShipStats(ShipStatStaticData shipStatStaticData)
        {
            _shipStatStaticData = shipStatStaticData;
        }
    }
}