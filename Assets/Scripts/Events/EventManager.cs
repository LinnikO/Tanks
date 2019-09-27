using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventManager
{
    private class MyObjectEvent : UnityEvent<object>
    { }

    private static Dictionary<EventType, UnityEvent<object>> eventListeners;

    static EventManager() {
        eventListeners = new Dictionary<EventType, UnityEvent<object>>();
    }

    public static void AddListener(EventType type, UnityAction<object> listener) {
        UnityEvent<object> thisEvent;
        if(eventListeners.TryGetValue(type, out thisEvent))    
        {         
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new MyObjectEvent();
            thisEvent.AddListener(listener);
            eventListeners.Add(type, thisEvent);
        }       
    }

    public static void RemoveListener(EventType type, UnityAction<object> listener) {
        UnityEvent<object> thisEvent;
        if(eventListeners.TryGetValue(type, out thisEvent))      
        {
            thisEvent.RemoveListener(listener);            
        }
    }

    public static void TriggerEvent(EventType type, object args) {
        UnityEvent<object> thisEvent;
        if (eventListeners.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke(args);
        }        
    }
}
