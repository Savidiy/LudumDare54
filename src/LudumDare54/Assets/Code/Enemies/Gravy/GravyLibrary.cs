using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(GravyLibrary), menuName = "Static Data/" + nameof(GravyLibrary),
        order = 0)]
    public sealed class GravyLibrary : AutoSaveScriptableObject
    {
        [ListDrawerSettings(ListElementLabelName = "@this")]
        public List<GravyStaticData> Gravys = new();

        public GravyStaticData Get(ShipType shipType)
        {
            foreach (GravyStaticData gravyStaticData in Gravys)
            {
                if (gravyStaticData.ShipType == shipType)
                    return gravyStaticData;
            }

            Debug.LogError($"Can't find Gravy static data with type '{shipType}'");
            return Gravys[0];
        }
    }

    [Serializable]
    public class GravyStaticData
    {
        public ShipType ShipType = ShipType.BigGravy1;
        public ShipBehaviour ShipBehaviourPrefab;
        public GravyStatsStaticData Stats;

        public override string ToString()
        {
            return ShipType.ToStringCached();
        }
    }

    [Serializable]
    public class GravyStatsStaticData
    {
        public float InsideRotationSpeed;
        public float ForwardSpeed;
        public int StartHealth;
        public int SelfDamageFromCollision;
        public float OutsideRotationPeriod;
        public float OutsideRotationDistance;
        public float OutsideRotationDelay = 1;

        [Title("Death Spawn")]
        public int MinSpawnCount;

        public List<DeathSpawnStaticData> DeathSpawnStaticData = new();
    }
}