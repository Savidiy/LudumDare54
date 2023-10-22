using System.Collections.Generic;

namespace LudumDare54
{
    public class EffectHolder
    {
        private readonly List<EffectView> _effects = new();
        public IReadOnlyList<EffectView> Effects => _effects;

        public void Add(EffectView effectView)
        {
            _effects.Add(effectView);
        }

        public void RemoveAt(int index)
        {
            _effects.RemoveAt(index);
        }

        public void CleanUp()
        {
            _effects.Clear();
        }
    }
}