using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [Serializable]
    public class DeathSpawnStaticData
    {
        public ShipType ShipType;
        [Range(0, 100)] public float SpawnChance;

        [MinMaxSlider(-360, 360)] public Vector2 MinMaxAngleRandom = new Vector2(-180, 180);

        public float PositionOffset;
    }
}