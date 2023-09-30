using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(ShipStatStaticDataLibrary), menuName = "Static Data/" + nameof(ShipStatStaticDataLibrary),
        order = 0)]
    public sealed class ShipStatStaticDataLibrary : AutoSaveScriptableObject
    {
        [FormerlySerializedAs("ShipStaticData")] [ListDrawerSettings(ListElementLabelName = "@this")]
        public List<ShipStatStaticData> ShipStatStaticData = new();

        public ValueDropdownList<string> ShipStatIds { get; } = new();

        public ShipStatStaticData Get(string shipId)
        {
            for (var index = 0; index < ShipStatStaticData.Count; index++)
            {
                ShipStatStaticData shipStatStaticData = ShipStatStaticData[index];
                if (string.Equals(shipStatStaticData.ShipStatId, shipId, StringComparison.InvariantCulture))
                    return shipStatStaticData;
            }

            throw new Exception($"ShipStaticData '{shipId}' not found");
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            ShipStatIds.Clear();
            foreach (ShipStatStaticData shipStaticData in ShipStatStaticData)
                ShipStatIds.Add(shipStaticData.ShipStatId);
        }
    }
}