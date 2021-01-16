using System.Collections.Generic;
using UnityEngine;

public static class EventDefine
{
    private static EventManager eventManager;
    private static EventManager EventManager
    {
        get
        {
            eventManager = Object.FindObjectOfType<EventManager>();
            if (eventManager == null)
            {
                initEventManager();
            }
            return eventManager;
        }
    }

    private static void initEventManager()
    {
        GameObject eventManagerObj = new GameObject("EventManager");
        eventManager = eventManagerObj.AddComponent<EventManager>();
    }

    private static HashSet<string> events = new HashSet<string>();

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="eventName"></param>
    public static void registerEvent(string eventName)
    {
        events.Add(eventName);
    }

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="eventName">事件</param>
    /// <param name="action">回调</param>
    public static void subscribeEvent(Object obj, string eventName, System.Action<object[]> func)
    {
        EventManager.subscribeEvent(obj, eventName, func);
    }

    /// <summary>
    /// 取消事件
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="eventName">事件</param>
    public static void unSubscribeEvent(Object obj, string eventName)
    {
        EventManager.unSubscribeEvent(obj, eventName);
    }

    /// <summary>
    /// 派发事件
    /// </summary>
    /// <param name="eventName">事件</param>
    public static void CallEvent(string eventName, params object[] parameters)
    {
        if (!events.Contains(eventName))
            return;

        EventManager.event2Func(eventName, parameters);
    }

}
