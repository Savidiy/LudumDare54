using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [Serializable]
    public sealed class ShipStatStaticData
    {
        public string ShipStatId = "";
        public float RotationSpeed;
        public float ForwardSpeed;
        public float BackwardSpeed;
        public float StrafeSpeed;
        public float ShootCooldown = 0.2f;
        public int ShootDamage = 1;
        public int StartHealth;
        public int SelfDamageFromCollision;
        public DeathActionData DeathActionData;

        public override string ToString()
        {
            return ShipStatId;
        }
    }

    [Serializable]
    public class DeathActionData
    {
        public int MinSpawnCount;
        public List<DeathSpawnStaticData> DeathSpawnStaticData = new();
    }

    [Serializable]
    public class DeathSpawnStaticData
    {
        [ValueDropdown(nameof(ShipIds))] public string ShipId;
        private ValueDropdownList<string> ShipIds => OdinShipIdsProvider.ShipIds;
        [Range(0, 100)] public float SpawnChance;

        [MinMaxSlider(-360, 360)] public Vector2 MinMaxAngleRandom = new Vector2(-180, 180);

        public float PositionOffset;
    }
}