
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListenManager : Singleton<ListenManager>
{
    private Dictionary<EventID, Action<object>> listeners = new Dictionary<EventID, Action<object>>();

    #region Register, Unregister, Broadcast
    public void Register(EventID id, Action<object> action)
    {
        if (action == null) { return; }
        if (listeners.ContainsKey(id))
        {
            if (listeners[id] != null)
                if (!listeners[id].GetInvocationList().Contains(action))
                    listeners[id] += action;
        }
        else
        {
            listeners.Add(id, (obj) => { });
            listeners[id] += action;
        }
    }
    public void Unregister(EventID id, Action<object> action)
    {

        if (listeners.ContainsKey(id) && action != null)
        {
            if (listeners[id].GetInvocationList().Contains(action))
                listeners[id] -= action;
        }
    }
    public void UnregisterAll(EventID id)
    {
        if (listeners.ContainsKey(id))
        {
            listeners.Remove(id);
        }
    }
    public void Broadcast(EventID id, object data)
    {
        if (listeners.ContainsKey(id))
        {
            listeners[id].Invoke(data);
        }
    }
    #endregion

}
public static class ListenerManagerExtension
{
    public static void Register(this MonoBehaviour listener, EventID id, Action<object> action)
    {
        if (ListenManager.HasInstance)
        {
            ListenManager.Instance.Register(id, action);
        }
    }
    public static void Unregister(this MonoBehaviour listener, EventID id, Action<object> action)
    {
        if (ListenManager.HasInstance)
        {
            ListenManager.Instance.Unregister(id, action);
        }
    }
    public static void UnregisterAll(this MonoBehaviour listener, EventID id)
    {
        if (ListenManager.HasInstance)
        {
            ListenManager.Instance.UnregisterAll(id);
        }
    }
    public static void Broadcast(this MonoBehaviour listener, EventID id)
    {
        if (ListenManager.HasInstance)
        {
            ListenManager.Instance.Broadcast(id, null);
        }
    }
    public static void Broadcast(this MonoBehaviour listener, EventID id, object data)
    {
        if (ListenManager.HasInstance)
        {
            ListenManager.Instance.Broadcast(id, data);
        }
    }
}