using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(ShipStaticDataLibrary), menuName = "Static Data/" + nameof(ShipStaticDataLibrary),
        order = 0)]
    public class ShipStaticDataLibrary : AutoSaveScriptableObject
    {
        public List<ShipStaticData> ShipStaticData = new();

        public ValueDropdownList<string> ShipIds { get; } = new();

        public ShipStaticData Get(string shipId)
        {
            for (var index = 0; index < ShipStaticData.Count; index++)
            {
                ShipStaticData shipStaticData = ShipStaticData[index];
                if (string.Equals(shipStaticData.ShipId, shipId, StringComparison.InvariantCulture))
                    return shipStaticData;
            }

            throw new Exception($"ShipStaticData '{shipId}' not found");
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            ShipIds.Clear();
            foreach (ShipStaticData shipStaticData in ShipStaticData)
                ShipIds.Add(shipStaticData.ShipId);
        }
    }
}