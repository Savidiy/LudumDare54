using System;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(SmallShipsSettings), menuName = "Static Data/" + nameof(SmallShipsSettings),
        order = 0)]
    public sealed class SmallShipsSettings : AutoSaveScriptableObject
    {
        public StupidCircleDudeData StupidCircleDudeData;
        public SimpleRunnerData SimpleRunnerData;
    }

    [Serializable]
    public class StupidCircleDudeData
    {
        public ShipBehaviour ShipBehaviourPrefab;
        public CommonSmallShipData CommonData;
        public StupidCircleDudeStatsData Stats;
    }

    [Serializable]
    public class CommonSmallShipData
    {
        public int StartHealth = 3;
        public int SelfDamageFromCollision = 1;
        public SoundIdData ShootSoundId;
        public SoundIdData HurtSoundId;
        public EffectType DeathExplosionType = EffectType.BigExplosion;
    }

    [Serializable]
    public class StupidCircleDudeStatsData
    {
        public float ForwardSpeed;
        public float MinRotateSpeed;
        public float MaxRotateSpeed;
        public float MinThinkingCooldown;
        public float MaxThinkingCooldown;
        public float MinShootCooldown;
        public float MaxShootCooldown;
        public float ForwardMoveAfterShootCooldown;
        [ValueDropdown(nameof(BulletIds))] public string BulletId;
        private ValueDropdownList<string> BulletIds => OdinBulletIdsProvider.BulletIds;
        public int BulletDamage = 1;
    }

    [Serializable]
    public class SimpleRunnerData
    {
        public ShipBehaviour ShipBehaviourPrefab;
        public CommonSmallShipData CommonData;
        public SimpleRunnerStatsData Stats;
    }

    [Serializable]
    public class SimpleRunnerStatsData
    {
        public float DefaultSpeed;
        public float MinRotateSpeed;
        public float MaxRotateSpeed;
        public float MinThinkingCooldown;
        public float MaxThinkingCooldown;
    }
}