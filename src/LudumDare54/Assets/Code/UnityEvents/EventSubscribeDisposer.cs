using System;

namespace LudumDare54
{
    public sealed class EventSubscribeDisposer : IDisposable
    {
        private UnityEventInvoker _unityEventInvoker;
        private readonly UnityEventType _eventTypeType;
        private Action _action;

        public EventSubscribeDisposer(UnityEventInvoker unityEventInvoker, UnityEventType eventTypeType, Action action)
        {
            _action = action;
            _eventTypeType = eventTypeType;
            _unityEventInvoker = unityEventInvoker;
        }

        public void Dispose()
        {
            if (_unityEventInvoker == null)
                return;

            _unityEventInvoker.Unsubscribe(_eventTypeType, _action);
            _unityEventInvoker = null;
            _action = null;
        }
    }
}