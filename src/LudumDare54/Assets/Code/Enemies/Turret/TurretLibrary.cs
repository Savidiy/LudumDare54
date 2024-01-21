using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(TurretLibrary), menuName = "Static Data/" + nameof(TurretLibrary))]
    public sealed class TurretLibrary : AutoSaveScriptableObject
    {
        [SerializeField, ListDrawerSettings(ListElementLabelName = "@this")]
        private List<TurretData> TurretStaticData = new();

        public TurretData Get(ShipType shipType)
        {
            foreach (TurretData staticData in TurretStaticData)
            {
                if (staticData.ShipType == shipType)
                    return staticData;
            }

            throw new KeyNotFoundException($"Can't find turret static data for ship type: {shipType}");
        }
    }

    [Serializable]
    public sealed class TurretData
    {
        public ShipType ShipType;
        public ShipBehaviour ShipBehaviourPrefab;
        public TurretStatsData Stats;

        public override string ToString()
        {
            return ShipType.ToStringCached();
        }
    }

    [Serializable]
    public class TurretStatsData
    {
        public int StartHealth = 1;
        public int SelfDamageFromCollision;
        public float RotationSpeed = 40;
        public float SingleShootCooldown = 0.15f;
        public float BurstCooldown = 1;
        public int BurstSize = 3;
        [ValueDropdown(nameof(BulletIds))] public string BulletId;
        private ValueDropdownList<string> BulletIds => OdinBulletIdsProvider.BulletIds;
        public int BulletDamage = 1;
        public float DetectDistance = 2;
        public bool NeedSeeHeroToShoot;
        public float AlarmDuration = 1;
        public bool HasAimAngle;
        public float AimAngleDegree = 1;

        public SoundIdData ShootSoundId;
        public SoundIdData HurtSoundId;
        public EffectType DeathExplosionType;
    }
}