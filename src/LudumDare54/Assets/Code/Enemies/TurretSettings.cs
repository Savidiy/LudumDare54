using System;

namespace LudumDare54
{
    [Serializable]
    public class TurretSettings
    {
        public float RotationSpeed = 40;
        public float SingleShootCooldown = 0.15f;
        public float BurstCooldown = 1;
        public int BurstSize = 3;
        public float BulletSpeed = 2;
        public float BulletLifeTime = 1;
        public int BulletDamage = 1;
        public float AttackRadius = 2;
        public float AttackAngle = 1;
        public float AlarmDuration = 1;
    }
}