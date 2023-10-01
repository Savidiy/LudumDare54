namespace LudumDare54
{
    public sealed class AsteroidStats
    {
        private readonly ShipStatStaticData _shipStatStaticData;
        
        public float ForwardSpeed => _shipStatStaticData.ForwardSpeed;
        public float RotationSpeed => _shipStatStaticData.RotationSpeed;

        public AsteroidStats(ShipStatStaticData shipStatStaticData)
        {
            _shipStatStaticData = shipStatStaticData;
        }
    }
}