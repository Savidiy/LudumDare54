using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(ShipStaticDataLibrary), menuName = "Static Data/" + nameof(ShipStaticDataLibrary),
        order = 0)]
    public sealed class ShipStaticDataLibrary : AutoSaveScriptableObject
    {
        [SerializeField, ListDrawerSettings(ListElementLabelName = "@this")]
        private List<ShipStaticData> Data = new();

        public ValueDropdownList<string> ShipIds { get; } = new();

        protected override void OnValidate()
        {
            base.OnValidate();
            ShipIds.Clear();
            foreach (ShipStaticData enemyStaticData in Data)
                ShipIds.Add(enemyStaticData.ShipId);
        }

        public ShipStaticData GetShipStaticData(string shipId)
        {
            foreach (ShipStaticData shipStaticData in Data)
            {
                if (string.Equals(shipStaticData.ShipId, shipId, StringComparison.InvariantCulture))
                    return shipStaticData;
            }

            throw new Exception($"Can't find ship static data with id '{shipId}'");
        }
    }

    [Serializable]
    public sealed class ShipStaticData
    {
        [FormerlySerializedAs("EnemyId")] public string ShipId = "";

        [ValueDropdown(nameof(ShipStatsIds))] public string StatId;
        private ValueDropdownList<string> ShipStatsIds => OdinShipStatIdsProvider.ShipStatIds;

        public ShipBehaviour ShipBehaviourPrefab;

        public override string ToString()
        {
            return ShipId;
        }
    }
}