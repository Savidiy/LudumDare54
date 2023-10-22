using UnityEngine;

namespace LudumDare54
{
    public sealed class SimpleDeathSetup : IDeathSettings
    {
        private readonly Transform _transform;

        public bool HasDeathEffect { get; } = true;
        public EffectType EffectType { get; } = EffectType.BigExplosion;
        public Vector3 Position => _transform.position;

        public SimpleDeathSetup(Transform transform)
        {
            _transform = transform;
        }
    }
}