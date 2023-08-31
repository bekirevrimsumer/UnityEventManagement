using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.AddListener("test", Test);
        EventManager.AddListener("testData", TestData);
        EventManager.AddListener("testPriority", TestPriority, 2); 
        EventManager.AddListener("testPriority", TestPriority2, 1);
        EventManager.AddListener("testDataPriority", TestDataPriority, 2);
    }
    
    
    private void Test()
    {
        Debug.Log("Test");
    }
    
    private void TestData(EventData data)
    {
        var testTitle = data.GetData<string>("test");
        Debug.Log("TestData: " + testTitle);
    }
    
    private void TestPriority()
    {
        Debug.Log("TestPriority");
    }
    
    private void TestPriority2()
    {
        Debug.Log("TestPriority2");
    }
    
    private void TestDataPriority(EventData data)
    {
        var testTitle = data.GetData<string>("test");
        Debug.Log("TestDataPriority: " + testTitle);
    }
    
    public void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 20), "Send Event"))
        {
            EventManager.TriggerEvent("test");
        }
        
        if (GUI.Button(new Rect(10, 40, 150, 20), "Send Event with Data"))
        {
            EventData eventData = new EventData();
            eventData.AddData("test", "Hello World");
            EventManager.TriggerEvent("testData", eventData);
            
        }
        
        if (GUI.Button(new Rect(10, 70, 150, 20), "Send Event with Priority"))
        {
            EventManager.TriggerEvent("testPriority");
        }
        
        
        if (GUI.Button(new Rect(10, 100, 150, 20), "Send Event with Data and Priority"))
        {
            EventData eventData = new EventData();
            eventData.AddData("test", "Hello World");
            EventManager.TriggerEvent("testDataPriority", eventData);
        }
    }
}
