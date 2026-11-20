using System;
using System.Collections.Generic;

public static class EventManager<TEvent>
{
    private static readonly Dictionary<TEvent, Delegate> listeners = new();
    public static void Subscribe<TData>(TEvent eventType, Action<TData> listener)
    {
        if (listener == null) return;
        if(listeners.TryGetValue(eventType, out Delegate currentListeners))
        {
            if (currentListeners is Action<TData> typedListeners)
                listeners[eventType] = typedListeners + listener;
            else
                throw new InvalidOperationException($"El evento {eventType} usa otro tipo de argumento");
        }
        else
        {
            listeners[eventType] = listener;
        }
    }
    
    public static void Unsubscribe<TData>(TEvent eventType, Action<TData> listener)
    {
        if(!listeners.TryGetValue(eventType, out Delegate currentListeners)) return;
        if (currentListeners is not Action<TData> typedListeners) return;
        typedListeners -= listener;
        if (typedListeners == null)
            listeners.Remove(eventType);
        else
            listeners[eventType] = typedListeners;
    }

    public static void Publish<TData>(TEvent eventType, TData data)
    {
        if(listeners.TryGetValue(eventType,out Delegate currentListeners) && 
            currentListeners is Action<TData> typedListeners)
        {
            typedListeners.Invoke(data);
        }
    }
}
