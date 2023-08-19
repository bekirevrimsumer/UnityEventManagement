using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, List<EventInfo>> _events;
    private Dictionary<string, List<EventInfo<EventData>>> _eventsWithData;

    private static EventManager _eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                else
                    _eventManager.Init();
            }

            return _eventManager;
        }
    }

    void Init()
    {
        if (_events == null)
        {
            _events = new Dictionary<string, List<EventInfo>>();
            _eventsWithData = new Dictionary<string, List<EventInfo<EventData>>>();
        }
    }

    public List<string> GetEventNames()
    {
        var eventNames = new List<string>();
        foreach (var eventName in _events.Keys)
        {
            eventNames.Add(eventName);
        }

        foreach (var eventName in _eventsWithData.Keys)
        {
            eventNames.Add(eventName);
        }

        return eventNames;
    }

    public List<string> GetListeners(string eventName)
    {
        var listeners = new List<string>();
        if (_events.TryGetValue(eventName, out var eventInfos))
        {
            foreach (var eventInfo in eventInfos)
            {
                listeners.Add(eventInfo.Event.GetPersistentMethodName(0));
            }
        }

        if (_eventsWithData.TryGetValue(eventName, out var eventInfosWithData))
        {
            foreach (var eventInfo in eventInfosWithData)
            {
                listeners.Add(eventInfo.Event.GetPersistentMethodName(0));
            }
        }

        return listeners;
    }

    public static void AddListener(string eventName, UnityAction listener, int? priority = 1)
    {
        if (string.IsNullOrEmpty(eventName) || listener == null)
        {
            Debug.LogError("Invalid event name or listener provided.");
            return;
        }

        if (!Instance._events.ContainsKey(eventName))
        {
            Instance._events.Add(eventName, new List<EventInfo>());
        }

        var eventInfo = new EventInfo(new UnityEvent(), priority.Value);
        eventInfo.Event.AddListener(listener);
        Instance._events[eventName].Add(eventInfo);
    }

    public static void AddListener(string eventName, UnityAction<EventData> listener, int? priority = 1)
    {
        if (string.IsNullOrEmpty(eventName) || listener == null)
        {
            Debug.LogError("Invalid event name or listener provided.");
            return;
        }

        if (!Instance._eventsWithData.ContainsKey(eventName))
        {
            Instance._eventsWithData.Add(eventName, new List<EventInfo<EventData>>());
        }

        var eventInfo = new EventInfo<EventData>(new UnityEvent<EventData>(), priority.Value);
        eventInfo.Event.AddListener(listener);
        Instance._eventsWithData[eventName].Add(eventInfo);
    }

    public static void RemoveListener(string eventName, UnityAction listener)
    {
        if (string.IsNullOrEmpty(eventName) || listener == null)
        {
            Debug.LogError("Invalid event name or listener provided.");
            return;
        }

        if (Instance._events.TryGetValue(eventName, out var eventInfos))
        {
            var eventInfo = eventInfos.Find(x => x.Event.GetPersistentMethodName(0) == listener.Method.Name);
            if (eventInfo != null)
            {
                eventInfos.Remove(eventInfo);
            }
        }
    }

    public static void RemoveListener(string eventName, UnityAction<EventData> listener)
    {
        if (string.IsNullOrEmpty(eventName) || listener == null)
        {
            Debug.LogError("Invalid event name or listener provided.");
            return;
        }

        if (Instance._events.TryGetValue(eventName, out var eventInfos))
        {
            var eventInfo = eventInfos.Find(x => x.Event.GetPersistentMethodName(0) == listener.Method.Name);
            if (eventInfo != null)
            {
                eventInfos.Remove(eventInfo);
            }
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if(string.IsNullOrEmpty(eventName))
        {
            Debug.LogError("Invalid event name provided.");
            return;
        }
        
        if (Instance._events.TryGetValue(eventName, out var eventInfos))
        {
            foreach (var eventInfo in eventInfos)
            {
                eventInfo.Event.Invoke();
            }
        }
    }

    public static void TriggerEvent(string eventName, EventData eventData)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            Debug.LogError("Invalid event name provided.");
            return;
        }
        
        if (Instance._eventsWithData.TryGetValue(eventName, out var eventInfos))
        {
            foreach (var eventInfo in eventInfos)
            {
                eventInfo.Event.Invoke(eventData);
            }
        }
    }
}