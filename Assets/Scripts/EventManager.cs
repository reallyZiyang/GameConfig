using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, Dictionary<Object, List<System.Action<object[]>>>> eventsCalls = new Dictionary<string, Dictionary<Object, List<System.Action<object[]>>>>();

        private void Awake()
        {

        }

        public void registerUIEvent(string eventName)
        {
            eventsCalls[eventName] = new Dictionary<Object, List<System.Action<object[]>>>();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="obj">物体</param>
        /// <param name="eventName">事件</param>
        /// <param name="action">回调</param>
        public void subscribeEvent(Object obj, string eventName, System.Action<object[]> func)
        {
            if (!eventsCalls[eventName].ContainsKey(obj))
                eventsCalls[eventName].Add(obj, new List<System.Action<object[]>>());
            eventsCalls[eventName][obj].Add(func);
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
        public void event2Func<T>(T eventName, params object[] parameters)
        {
            var functions = eventsCalls[eventName.ToString()].Values;
            foreach (var funcList in functions)
            {
                foreach (var func in funcList)
                {
                    func(parameters);
                }
            }
        }
    }
}
