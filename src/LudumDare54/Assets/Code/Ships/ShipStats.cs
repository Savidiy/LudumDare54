namespace LudumDare54
{
    public sealed class ShipStats : IShipStats
    {
        private readonly ShipStaticData _shipStaticData;
        public float RotationSpeed => _shipStaticData.RotationSpeed;
        public float ForwardSpeed => _shipStaticData.ForwardSpeed;
        public float BackwardSpeed => _shipStaticData.BackwardSpeed;
        public float StrafeSpeed => _shipStaticData.StrafeSpeed;

        public ShipStats(ShipStaticData shipStaticData)
        {
            _shipStaticData = shipStaticData;
        }
    }
}