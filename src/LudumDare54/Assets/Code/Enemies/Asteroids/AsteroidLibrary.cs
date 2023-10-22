using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(AsteroidLibrary), menuName = "Static Data/" + nameof(AsteroidLibrary))]
    public sealed class AsteroidLibrary : AutoSaveScriptableObject
    {
        [ListDrawerSettings(ListElementLabelName = "@this")]
        public List<AsteroidStaticData> Asteroids = new();

        public AsteroidStaticData Get(ShipType shipType)
        {
            foreach (AsteroidStaticData asteroidStaticData in Asteroids)
            {
                if (asteroidStaticData.ShipType == shipType)
                    return asteroidStaticData;
            }

            Debug.LogError($"Can't find asteroid static data with type '{shipType}'");
            return Asteroids[0];
        }
    }

    [Serializable]
    public class AsteroidStaticData
    {
        public ShipType ShipType = ShipType.BigAsteroid1;
        public ShipBehaviour ShipBehaviourPrefab;
        public AsteroidStatsStaticData Stats;

        public override string ToString()
        {
            return ShipType.ToStringCached();
        }
    }

    [Serializable]
    public class AsteroidStatsStaticData
    {
        public float RotationSpeed;
        public float ForwardSpeed;
        public int StartHealth;
        public int SelfDamageFromCollision;
        [InlineProperty] public SoundIdData HurtSoundId;

        [Title("Death Spawn")]
        public int MinSpawnCount;

        public List<DeathSpawnStaticData> DeathSpawnStaticData = new();
        public EffectType DeathExplosionType;
    }
}