﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class WindowBase : MonoBehaviour
    {
        protected GameObject windowPrefab;

        public virtual void init()
        {
            windowPrefab = this.gameObject;
            Debug.Log(windowPrefab.name + " init");
        }

        /// <summary>
        /// 获取子级的某个物体
        /// </summary>
        /// <param name="childName">物体名字</param>
        /// <returns></returns>
        public Transform child(string childName, Transform parent = null)
        {
            parent = parent ? parent : root();
            Transform res = parent.Find(childName);
            if (res)
                return res;

            for (int index = 0; index < parent.childCount; index++)
            {
                res = child(childName, parent.GetChild(index));
                if (res)
                    return res;
            }
            return res;
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        public Transform root()
        {
            return windowPrefab.transform;
        }

        /// <summary>
        /// 改名字
        /// </summary>
        /// <param name="name">名字</param>
        public void rename(string name)
        {
            root().name = name;
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        public void show()
        {
            onShow();
            windowPrefab.SetActive(true);
        }
        public virtual void show(params object[] parameters)
        {
            onShow(parameters);
            windowPrefab.SetActive(true);
        }

        public void close()
        {
            onClose();
            windowPrefab.SetActive(false);
        }

        /// <summary>
        /// 打开时调用
        /// </summary>
        protected virtual void onShow(params object[] parameters) 
        { 
            if (windowPrefab == null)
            {
                init();
            }
        }

        /// <summary>
        /// 关闭时调用
        /// </summary>
        protected void onClose() { }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="obj">物体</param>
        /// <param name="eventName">事件</param>
        /// <param name="action">回调</param>
        public void subscribe(Transform child, UIEvent eventName, System.Action<object[]> func)
        {
            if (child == null)
                return;

            EventDefine.subscribeEvent(child, eventName, func);
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="obj">物体</param>
        /// <param name="eventName">事件</param>
        public void unSubscribe(Transform child, UIEvent eventName)
        {
            if (child == null)
                return;

            EventDefine.unSubscribeEvent(child, eventName.ToString());
        }

    }
}

