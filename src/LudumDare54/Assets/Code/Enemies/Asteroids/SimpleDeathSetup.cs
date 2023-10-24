using UnityEngine;

namespace LudumDare54
{
    public sealed class SimpleDeathSetup : IDeathSetup
    {
        private readonly Transform _transform;

        public bool HasDeathEffect { get; }
        public EffectType EffectType { get; }
        public Vector3 Position => _transform.position;

        public SimpleDeathSetup(Transform transform, EffectType effectType)
        {
            _transform = transform;
            EffectType = effectType;
            HasDeathEffect = effectType != EffectType.None;
        }
    }
}