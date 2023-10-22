using System;

namespace LudumDare54
{
    public class EffectUpdater : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly EffectHolder _effectHolder;
        private readonly EffectPool _effectPool;
        private IDisposable _updateSubscribe;

        public EffectUpdater(IEventInvoker eventInvoker, EffectHolder effectHolder, EffectPool effectPool)
        {
            _eventInvoker = eventInvoker;
            _effectHolder = effectHolder;
            _effectPool = effectPool;
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
        }

        private void OnUpdate()
        {
            float deltaTime = _eventInvoker.DeltaTime;
            for (int index = _effectHolder.Effects.Count - 1; index >= 0; index--)
            {
                EffectView effectView = _effectHolder.Effects[index];
                effectView.Update(deltaTime);

                if (effectView.IsPlaying)
                    continue;

                _effectHolder.RemoveAt(index);
                _effectPool.Return(effectView);
            }
        }
    }
}