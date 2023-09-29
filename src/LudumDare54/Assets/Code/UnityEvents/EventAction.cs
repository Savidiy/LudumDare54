using System;

namespace LudumDare54
{
    public sealed class EventAction : IDisposable
    {
        public Action Action;
        public bool IsDisposed { get; private set; }

        public EventAction(Action action)
        {
            Action = action;
        }

        public void Dispose()
        {
            Action = null;
            IsDisposed = true;
        }
    }
}