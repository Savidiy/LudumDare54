using System;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LudumDare54
{
    public sealed class UnityEventInvoker : IEventInvoker, IDisposable
    {
        private readonly EventBehaviour _eventBehaviour = new GameObject("UnityEventInvoker").AddComponent<EventBehaviour>();
        private bool _isDisposed;
        public float DeltaTime => Time.deltaTime;

        public IDisposable Subscribe(UnityEventType eventTypeType, Action action)
        {
            if (_isDisposed)
                return new CompositeDisposable();

            _eventBehaviour.Add(eventTypeType, action);
            return new EventSubscribeDisposer(this, eventTypeType, action);
        }

        public void Unsubscribe(UnityEventType eventTypeType, Action action)
        {
            if (_isDisposed)
                return;

            _eventBehaviour.Remove(eventTypeType, action);
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _eventBehaviour.Clear();
            Object.Destroy(_eventBehaviour.gameObject);
        }
    }
}