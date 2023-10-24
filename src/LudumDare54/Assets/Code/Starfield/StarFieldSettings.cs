using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(StarFieldSettings), menuName = "Static Data/" + nameof(StarFieldSettings))]
    public sealed class StarFieldSettings : AutoSaveScriptableObject
    {
        public List<StarBehaviour> StarPrefabs = new();
        public List<Color[]> StarColors = new();
        [InlineButton(nameof(IncreaseColors), "Increase All")] public Color AddColor;
        [InlineButton(nameof(DecreaseColors), "Decrease All")] public Color RemoveColor;

        public float MinStarMoveMultiplier = 0.1f;
        public float MaxStarMoveMultiplier = 0.5f;
        public int StarCount = 100;
        public int StarMoveFrameRate = 20;

        public float StarBlinkPeriod = 0.01f;


        private void IncreaseColors()
        {
            for (var i = 0; i < StarColors.Count; i++)
            {
                Color[] starColor = StarColors[i];
                for (var index = 0; index < starColor.Length; index++)
                {
                    Color color = starColor[index];
                    color.r += AddColor.r;
                    color.g += AddColor.g;
                    color.b += AddColor.b;
                    color.a += AddColor.a;
                    starColor[index] = color;
                }
            }
        }

        private void DecreaseColors()
        {
            for (var i = 0; i < StarColors.Count; i++)
            {
                Color[] starColor = StarColors[i];
                for (var index = 0; index < starColor.Length; index++)
                {
                    Color color = starColor[index];
                    color.r -= RemoveColor.r;
                    color.g -= RemoveColor.g;
                    color.b -= RemoveColor.b;
                    color.a -= RemoveColor.a;
                    starColor[index] = color;
                }
            }
        }
    }
}