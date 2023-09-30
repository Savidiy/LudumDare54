using UnityEngine;

namespace LudumDare54
{
    public struct BulletData
    {
        public readonly Vector3 GunPosition;
        public readonly Quaternion Rotation;

        public BulletData(Vector3 gunPosition, Quaternion rotation)
        {
            GunPosition = gunPosition;
            Rotation = rotation;
        }
    }
}