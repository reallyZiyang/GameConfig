using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class WidgetBase : MonoBehaviour
    {
        protected GameObject widgetPrefab;
        protected Type selfType;
        public virtual void init()
        {
            widgetPrefab = this.gameObject;
            selfType = GetType();
            Debug.Log(widgetPrefab.name + " init");
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
            return widgetPrefab.transform;
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
        public virtual void show(params object[] parameters)
        {
            onShow(parameters);
            widgetPrefab.SetActive(true);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void destroy()
        {
            onDestroy();
            UIManager.Instance.destroyWidget(this);
        }

        /// <summary>
        /// 打开时调用
        /// </summary>
        protected virtual void onShow(params object[] parameters) 
        { 
            if (widgetPrefab == null)
            {
                init();
            }
        }

        /// <summary>
        /// 关闭时调用
        /// </summary>
        protected void onDestroy() { }

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

        public Type getType()
        {
            return selfType;
        }

    }
}

