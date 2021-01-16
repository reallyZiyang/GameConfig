using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<Object, System.Action<object[]>>> eventsCalls = new Dictionary<string, Dictionary<Object, System.Action<object[]>>>();

    private void Awake()
    {
        
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="eventName">事件</param>
    /// <param name="action">回调</param>
    public void subscribeEvent(Object obj, string eventName, System.Action<object[]> func)
    {
        if (!eventsCalls.ContainsKey(eventName))
        {
            eventsCalls.Add(eventName, new Dictionary<Object, System.Action<object[]>>());
        }

        eventsCalls[eventName].Add(obj, func);
    }

    /// <summary>
    /// 取消事件
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="eventName">事件</param>
    /// <param name="action">回调</param>
    public void unSubscribeEvent(Object obj, string eventName)
    {
        if (!eventsCalls.ContainsKey(eventName))
        {
            return;
        }
        if (!eventsCalls[eventName].ContainsKey(obj))
        {
            return;
        }

        eventsCalls[eventName].Remove(obj);
    }

    /// <summary>
    /// 根据事件调用回调
    /// </summary>
    /// <param name="eventName">事件</param>
    public void event2Func(string eventName, params object[] parameters)
    {
        var functions = eventsCalls[eventName].Values;
        foreach (var func in functions)
        {
            func(parameters);
        }
    }

}