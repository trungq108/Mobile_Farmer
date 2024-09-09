using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<Type, List<Action<object>>> eventHandlers = new Dictionary<Type, List<Action<object>>>();

    public static void AddListener<T>(Action<T> handler)
    {
        Type eventType = typeof(T);
        if (!eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType] = new List<Action<object>>();
        }
        eventHandlers[eventType].Add((e) => handler((T)e));
    }

    public static void RemoveListener<T>(Action<T> handler)
    {
        Type eventType = typeof(T);
        if (eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType].Remove((e) => handler((T)e));
            if (eventHandlers[eventType].Count == 0)
            {
                eventHandlers.Remove(eventType);
            }
        }
    }

    public static void TriggerEvent<T>(T eventData)
    {
        Type eventType = typeof(T);
        if (eventHandlers.ContainsKey(eventType))
        {
            foreach (var handler in eventHandlers[eventType])
            {
                handler(eventData);
            }
        }
    }
}


// eventType
public class SowSeed
{
    public Vector3[] collisionPositions;
}

public class WaterSeed 
{
    public Vector3[] collisionPositions;
}

public class FieldSown
{
    public CropField cropField;
}

public class FieldWatered
{
    public CropField cropField;
}

public class ChangeTool
{
    public Tool toolChange;
}



