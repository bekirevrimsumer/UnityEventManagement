using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventManager))]
public class EventManagerEditor : Editor
{
    private SerializedProperty _eventsProperty;

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
        
        //event list
        var eventNames = eventManager.GetEventNames();
        foreach (var eventName in eventNames)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(eventName);
            EditorGUILayout.EndHorizontal();
            
            //event listeners
            var listeners = eventManager.GetListeners(eventName);
            foreach (var listener in listeners)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(listener);
                EditorGUILayout.EndHorizontal();
            }
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
