using System;

namespace LudumDare54
{
    [Serializable]
    public sealed class ShipStaticData
    {
        public string ShipId;
        public float RotationSpeed;
        public float ForwardSpeed;
        public float BackwardSpeed;
        public float StrafeSpeed;

        public override string ToString()
        {
            return ShipId;
        }
    }
}