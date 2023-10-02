using System;

namespace LudumDare54
{
    [Serializable]
    public sealed class Progress
    {
        public int CurrentLevelIndex;
        public int HeroDeathCount;
        public int BulletCount;
        public int BulletHitCount;
        public int EnemiesKillCount;
        public int BumperHitCount;
    }
}