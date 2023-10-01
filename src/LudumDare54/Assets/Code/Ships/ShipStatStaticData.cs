using System;
using UnityEngine.Serialization;

namespace LudumDare54
{
    [Serializable]
    public sealed class ShipStatStaticData
    {
        [FormerlySerializedAs("ShipId")] public string ShipStatId = "";
        public float RotationSpeed;
        public float ForwardSpeed;
        public float BackwardSpeed;
        public float StrafeSpeed;
        public float ShootCooldown = 0.2f;
        public int ShootDamage = 1;
        public int StartHealth;

        public override string ToString()
        {
            return ShipStatId;
        }
    }
}