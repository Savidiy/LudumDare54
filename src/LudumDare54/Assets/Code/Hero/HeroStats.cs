﻿namespace LudumDare54
{
    public sealed class HeroStats
    {
        private readonly HeroSettings _heroSettings;

        public float ShootCooldown => _heroSettings.ShootCooldown;
        public int Damage => _heroSettings.ShootDamage;
        public float RotationSpeed => _heroSettings.RotationSpeed;
        public float ForwardSpeed => _heroSettings.ForwardSpeed;
        public float BackwardSpeed => _heroSettings.BackwardSpeed;
        public float StrafeSpeed => _heroSettings.StrafeSpeed;
        public string BulletId => _heroSettings.BulletId;
        public float SingleShootTemperature => _heroSettings.SingleShootTemperature;
        public float OverheatMaxTemperature => _heroSettings.OverheatMaxTemperature;
        public float AfterShootCoolingPause => _heroSettings.AfterShootCooldown;
        public float TemperatureCoolingPerSecond => _heroSettings.TemperatureCoolingPerSecond;
        public float OverheatShootCooldownBonus => _heroSettings.OverheatShootCooldownBonus;

        public HeroStats(HeroSettings heroSettings)
        {
            _heroSettings = heroSettings;
        }
    }
}