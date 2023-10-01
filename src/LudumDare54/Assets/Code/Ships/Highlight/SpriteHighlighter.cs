using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    public sealed class SpriteHighlighter : MonoBehaviour
    {
        private static readonly int WhitePercentProperty = Shader.PropertyToID("_WhitePercent");

        [SerializeField] private SpriteRenderer[] SpriteRenderers;

        private float _whitePercent;
        private Sequence _sequence;
        private Material[] _materials;

        public void Awake()
        {
            _materials = new Material[SpriteRenderers.Length];
            for (var index = 0; index < SpriteRenderers.Length; index++)
                _materials[index] = SpriteRenderers[index].material;
        }
        
        [Button]
        private void CollectRenderers()
        {
            SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void Flash(HighlightSettings highlightSettings)
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            _sequence.Append(DOTween.To(
                    () => _whitePercent,
                    SetWhitePercent,
                    highlightSettings.MaxValue,
                    highlightSettings.StartBlinkDuration)
                .SetEase(highlightSettings.StartEase));

            _sequence.AppendInterval(highlightSettings.PauseDuration);

            _sequence.Append(DOTween.To(
                    () => _whitePercent,
                    SetWhitePercent,
                    0f,
                    highlightSettings.EndBlinkDuration)
                .SetEase(highlightSettings.EndEase));
        }

        private void SetWhitePercent(float value)
        {
            _whitePercent = value;

            for (var index = 0; index < _materials.Length; index++)
            {
                Material material = _materials[index];
                material.SetFloat(WhitePercentProperty, value);
            }
        }

        public void Dispose()
        {
            _sequence?.Kill();
            _sequence = null;
        }
    }
}