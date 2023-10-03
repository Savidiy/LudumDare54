using DG.Tweening;
using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(HighlightSettings), menuName = "Static Data/" + nameof(HighlightSettings))]
    public sealed class HighlightSettings : AutoSaveScriptableObject
    {
        public float StartBlinkDuration = 0.05f;
        public Ease StartEase = Ease.Linear;
        public float MaxValue = 1f;
        public float PauseDuration = 0f;
        public float EndBlinkDuration = 0.1f;
        public Ease EndEase = Ease.Linear;
    }
}