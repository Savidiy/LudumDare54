using UnityEngine;

namespace LudumDare54
{
    public struct BulletData
    {
        public readonly Vector3 GunPosition;
        public readonly Quaternion Rotation;
        public readonly IShipDamage Damage;

        public BulletData(Vector3 gunPosition, Quaternion rotation, IShipDamage damage)
        {
            GunPosition = gunPosition;
            Rotation = rotation;
            Damage = damage;
        }
    }
}