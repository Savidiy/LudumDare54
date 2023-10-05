using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(RadarSettings), menuName = "Static Data/" + nameof(RadarSettings))]
    public sealed class RadarSettings : AutoSaveScriptableObject
    {
        public float TurnOnSpeed = 1;
        public float TurnOffSpeed = 1;
        public List<RadarLightData> RadarLightData = new();
    }

    [Serializable]
    public class RadarLightData
    {
        [MinMaxSlider(-360, 360, true)] public Vector2 Angles = new(-90, 90);
    }
}