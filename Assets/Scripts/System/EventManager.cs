using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventID { START=0, Normal, END}

public class EventManager : MonoSingleton<EventManager>
{
    Dictionary<EventID, Dictionary<string, Action<string>>> actions = new Dictionary<EventID, Dictionary<string, Action<string>>>();

    private void Awake()
    {
        for (EventID i = EventID.START + 1; i < EventID.END; i++)
        {
            actions[i] = new Dictionary<string, Action<string>>();
        }
    }

    public void AddEvent(EventID id, string key, Action<string> action)
    {
        if (actions[id].ContainsKey(key))
        {
            actions[id][key] += action;
        }
        else
        {
            actions[id][key] = action;
        }
    }

    public void RemoveEvent(EventID id, string key, Action<string> action)
    {
        if (actions[id].ContainsKey(key))
        {
            actions[id][key] -= action;
        }
    }

    public void Trigger(EventID id, string key)
    {
        if (actions[id].ContainsKey(key))
        {
            actions[id][key]?.Invoke(key);
            actions[id].Remove(key);
        }
    }
}

