using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<UIManager>();
                return instance;
            }
        }

        private Dictionary<string, WindowBase> windowsDic = new Dictionary<string, WindowBase>();

        /// <summary>
        /// 加载window
        /// </summary>
        /// <param name="name"></param>
        public void preLoadWindow(string name)
        {
            GameObject windowPrefab = Resources.Load<GameObject>("Windows/" + name);
            windowPrefab.SetActive(false);
            windowsDic.Add(name, windowPrefab.GetComponent<WindowBase>());
        }

        /// <summary>
        /// 获取window引用
        /// </summary>
        /// <param name="name">window名字</param>
        public WindowBase getWindow(string name)
        {
            if (!windowsDic.ContainsKey(name))
            {
                preLoadWindow(name);
            }

            WindowBase window = windowsDic[name];
            //window.init();
            return window;
        }

        /// <summary>
        /// 打开window
        /// </summary>
        /// <param name="name">window名字</param>
        public WindowBase openWindow(string name)
        {
            if (!windowsDic.ContainsKey(name))
            {
                preLoadWindow(name);
            }

            WindowBase window = windowsDic[name];
            window.show();
            return window;
        }

    }
}