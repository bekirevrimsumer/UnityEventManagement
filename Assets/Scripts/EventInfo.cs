using System.Collections.Generic;
using UnityEngine.Events;

public class BaseEventInfo
{
    public int Priority { get; private set; } = 1;

    public BaseEventInfo(int priority)
    {
        Priority = priority;
    }

    public void SetPriority(int priority)
    {
        Priority = priority;
    }
}

public class EventInfo : BaseEventInfo
{
    public UnityEvent Event { get; private set; }

    public EventInfo(UnityEvent unityEvent) : base(1)
    {
        Event = unityEvent;
    }

    public EventInfo(UnityEvent unityEvent, int priority) : base(priority)
    {
        Event = unityEvent;
    }
}

public class EventInfo<T> : BaseEventInfo where T : EventData
{
    public UnityEvent<T> Event { get; private set; }

    public EventInfo(UnityEvent<T> unityEvent) : base(1)
    {
        Event = unityEvent;
    }

    public EventInfo(UnityEvent<T> unityEvent, int priority) : base(priority)
    {
        Event = unityEvent;
    }
}