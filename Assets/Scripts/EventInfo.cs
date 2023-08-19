using UnityEngine.Events;

public class EventInfo
{
    public UnityEvent Event { get; private set; }
    public int Priority { get; private set; } = 1; //TR: Öncelik değeri yüksek olan event daha önce çalışır. / EN: The event with a higher priority value runs first.

    public EventInfo(UnityEvent unityEvent)
    {
        Event = unityEvent;
    }

    public EventInfo(UnityEvent unityEvent, int priority)
    {
        Event = unityEvent;
        Priority = priority;
    }
}

public class EventInfo<T> where T : EventData
{
    public UnityEvent<T> Event { get; private set; }
    public int Priority { get; private set; } = 1;

    public EventInfo(UnityEvent<T> unityEvent)
    {
        Event = unityEvent;
    }

    public EventInfo(UnityEvent<T> unityEvent, int priority)
    {
        Event = unityEvent;
        Priority = priority;
    }
}