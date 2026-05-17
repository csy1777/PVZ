using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : SingleTon<EventCenter>
{
    private Dictionary<string,UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();

    public void AddEvent(string eventName, UnityAction<object> action)
    {
        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, action);
        }
        else
        {
            eventDic[eventName] += action;
        }
    }
    

    public void RemoveEvent(string eventName, UnityAction<object> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic[eventName] -= action;
        }
    }

    public void ClearEvent()
    {
        eventDic.Clear();
    }

    public void EventTrigger(string eventName,object info)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic[eventName]?.Invoke(info);
        }
    }

    public void EventTrigger(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic[eventName]?.Invoke(null);
        }
    }
}
