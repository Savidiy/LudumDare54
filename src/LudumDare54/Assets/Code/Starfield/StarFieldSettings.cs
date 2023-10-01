using System.Collections.Generic;
using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(StarFieldSettings), menuName = "Static Data/" + nameof(StarFieldSettings),
        order = 0)]
    public sealed class StarFieldSettings : AutoSaveScriptableObject
    {
        public List<StarBehaviour> StarPrefabs = new();
        public List<Color> StarColors = new();

        public float MinStarMoveMultiplier = 0.1f;
        public float MaxStarMoveMultiplier = 0.5f;
        public int StarCount = 100;
        public int StarMoveFrameRate = 20;
    }
}