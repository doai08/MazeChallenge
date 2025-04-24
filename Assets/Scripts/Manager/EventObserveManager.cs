using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventObserveManager : Singleton<EventObserveManager>
{

    private Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();


    public void Subscribe(string eventName, Action<object> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = delegate { };

        eventDictionary[eventName] += listener;
    }

    public void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] -= listener;
    }

    public void TriggerEvent(string eventName, object eventData = null)
    {
        Debug.Log(eventName);
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName]?.Invoke(eventData);
    }
}