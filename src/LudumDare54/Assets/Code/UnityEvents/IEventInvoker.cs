using System;

namespace LudumDare54
{
    public interface IEventInvoker
    {
        IDisposable Subscribe(UnityEventType eventTypeType, Action action);
        float DeltaTime { get; }
    }
}