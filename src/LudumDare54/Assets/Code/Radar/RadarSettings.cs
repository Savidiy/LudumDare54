using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(RadarSettings), menuName = "Static Data/" + nameof(RadarSettings), order = 0)]
    public sealed class RadarSettings : AutoSaveScriptableObject
    {
        public RadarPointBehaviour RadarPointPrefab;
        [Min(0)] public float Scale = 0.05f;
        [Min(0)] public float MinOffset = 1;
    }
}