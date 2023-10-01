using UnityEngine;

namespace LudumDare54
{
    public struct BulletData
    {
        public readonly Vector3 StartPosition;
        public readonly Quaternion Rotation;
        public readonly IShipDamage Damage;

        public BulletData(Vector3 startPosition, Quaternion rotation, IShipDamage damage)
        {
            StartPosition = startPosition;
            Rotation = rotation;
            Damage = damage;
        }
    }
}