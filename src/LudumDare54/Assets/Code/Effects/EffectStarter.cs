using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LudumDare54
{
    public sealed class EffectStarter : IDisposable
    {
        private readonly EffectPool _effectPool;
        private readonly EffectHolder _effectHolder;
        private readonly HeroShipHolder _heroShipHolder;

        public EffectStarter(EffectPool effectPool, EffectHolder effectHolder, HeroShipHolder heroShipHolder)
        {
            _effectPool = effectPool;
            _effectHolder = effectHolder;
            _heroShipHolder = heroShipHolder;

            TestEffectBridge.Register(this);
        }

        public void ShowEffect(EffectType effectType, Vector3 position)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            EffectView effectView = _effectPool.Get(effectType, position, rotation);
            _effectHolder.Add(effectView);
        }

        public void TestEffect(EffectType effectType)
        {
            Vector3 position = _heroShipHolder.TryGetHeroShip(out Ship ship) ? ship.Position : Vector3.zero;
            ShowEffect(effectType, position);
        }

        public void CleanUp()
        {
            for (var index = 0; index < _effectHolder.Effects.Count; index++)
            {
                EffectView effectView = _effectHolder.Effects[index];
                _effectPool.Return(effectView);
            }

            _effectHolder.CleanUp();
        }

        public void Dispose()
        {
            TestEffectBridge.Unregister();
        }
    }
}