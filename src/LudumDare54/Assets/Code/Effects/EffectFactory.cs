using UnityEngine;

namespace LudumDare54
{
    public sealed class EffectFactory
    {
        private const string ADDRESS = "Effect";
        private readonly EffectLibrary _effectLibrary;
        private readonly AssetProvider _assetProvider;

        public EffectFactory(EffectLibrary effectLibrary, AssetProvider assetProvider)
        {
            _effectLibrary = effectLibrary;
            _assetProvider = assetProvider;
        }
        
        public EffectView Create(EffectType effectType, Transform transform)
        {
            var prefab = _assetProvider.GetPrefab<EffectBehaviour>(ADDRESS);
            EffectBehaviour effectBehaviour = Object.Instantiate(prefab, transform);
            AnimationData animationData = _effectLibrary.GetAnimationData(effectType);
            var effectView = new EffectView(effectBehaviour, animationData, effectType);
            return effectView;
        }
    }
}