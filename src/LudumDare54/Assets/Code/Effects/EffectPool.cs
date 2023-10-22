using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public class EffectPool
    {
        private readonly EffectFactory _effectFactory;
        private readonly Dictionary<EffectType, List<EffectView>> _effects = new();
        private readonly Transform _root;

        public EffectPool(EffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _root = new GameObject("Effects").transform;
        }

        public EffectView Get(EffectType effectType, Vector3 position, Quaternion rotation)
        {
            EffectView effectView = Get(effectType);
            effectView.SetPosition(position);
            effectView.SetRotation(rotation);
            effectView.Play();
            effectView.SetActive(true);
            return effectView;
        }

        private EffectView Get(EffectType effectType)
        {
            if (_effects.TryGetValue(effectType, out List<EffectView> effectViews))
            {
                if (effectViews.Count > 0)
                {
                    int index = effectViews.Count - 1;
                    EffectView effectView = effectViews[index];
                    effectViews.RemoveAt(index);
                    return effectView;
                }
            }

            EffectView view = _effectFactory.Create(effectType, _root);
            return view;
        }

        public void Return(EffectView effectView)
        {
            effectView.SetActive(false);
            if (_effects.TryGetValue(effectView.EffectType, out List<EffectView> effectViews))
            {
                effectViews.Add(effectView);
            }
            else
            {
                effectViews = new List<EffectView> {effectView};
                _effects.Add(effectView.EffectType, effectViews);
            }
        }
    }
}