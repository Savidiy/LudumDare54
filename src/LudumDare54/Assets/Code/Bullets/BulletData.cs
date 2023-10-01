using UnityEngine;

namespace LudumDare54
{
    public struct BulletData
    {
        public readonly string BulletId;
        public readonly Vector3 StartPosition;
        public readonly Quaternion Rotation;
        public readonly IShipDamage Damage;

        public BulletData(string bulletId, Vector3 startPosition, Quaternion rotation, IShipDamage damage)
        {
            BulletId = bulletId;
            StartPosition = startPosition;
            Rotation = rotation;
            Damage = damage;
        }
    }
}