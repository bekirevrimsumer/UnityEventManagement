using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventManager))]
public class EventManagerEditor : Editor
{
    private SerializedProperty _eventsProperty;
       private string _searchEventName = "";

    private void OnEnable()
    {
        _eventsProperty = serializedObject.FindProperty("_events");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        EditorGUILayout.Space(10); 

        EventManager eventManager = (EventManager)target;

        EditorGUILayout.LabelField("Event List", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        _searchEventName = EditorGUILayout.TextField("Search Event:", _searchEventName);
        EditorGUILayout.EndHorizontal();
        
        //event list
        var eventNames = eventManager.GetEventNames();
        foreach (var eventName in eventNames)
        {
            if (!eventName.ToLower().Contains(_searchEventName.ToLower()))
                continue;

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField($"Event Name: {eventName}", EditorStyles.boldLabel);

            var eventInfos = eventManager.GetEventInfos(eventName);
            foreach (var eventInfo in eventInfos)
            {
                DrawEventInfo(eventInfo);
            }
            
            EditorGUILayout.EndVertical();
        }
        
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawEventInfo(BaseEventInfo eventInfo)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField($"Priority: {eventInfo.Priority}");
        
        if (eventInfo is EventInfo)
        {
            EditorGUILayout.LabelField($"Standard Event");
        }
        else if (eventInfo is EventInfo<EventData>)
        {
            EventInfo<EventData> eventDataEventInfo = (EventInfo<EventData>)eventInfo;
            EditorGUILayout.LabelField($"Event with Data of Type: {eventDataEventInfo.Event.GetType().GetGenericArguments()[0]}");
        }
        
        EditorGUILayout.EndHorizontal();
    }
}
