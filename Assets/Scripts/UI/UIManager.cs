using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameConfiger
{
    Game_base,
    Tetris
}

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
        private Dictionary<string, WidgetBase> widgetsDic = new Dictionary<string, WidgetBase>();
        private Dictionary<string, Queue<GameObject>> widgetsPool = new Dictionary<string, Queue<GameObject>>();

        private AssetBundle manifestAB;
        private AssetBundleManifest manifest;
        /// <summary>
        /// 加载window
        /// </summary>
        /// <param name="name"></param>
        public void preLoadWindow(GameConfiger game, string name)
        {
            GameObject windowPrefab = AssetManager.Instance.loadWindowAsset(game, name);
            windowsDic.Add(name, windowPrefab.GetComponent<WindowBase>());
        }

        /// <summary>
        /// 加载widget
        /// </summary>
        /// <param name="name"></param>
        public void preLoadWidget(GameConfiger game, string name)
        {
            if (!widgetsDic.ContainsKey(name))
            {
                GameObject widgetPrefab = AssetManager.Instance.loadWindowAsset(game, name);
                widgetsDic.Add(name, widgetPrefab.GetComponent<WidgetBase>());
                if (!widgetsPool.ContainsKey(name))
                    widgetsPool.Add(name, new Queue<GameObject>());
                widgetsPool[name].Enqueue(widgetPrefab);
            }
        }

        /// <summary>
        /// 获取window引用
        /// </summary>
        /// <param name="name">window名字</param>
        public WindowBase getWindow(GameConfiger game, string name)
        {
            if (!windowsDic.ContainsKey(name))
            {
                preLoadWindow(game, name);
            }

            WindowBase window = windowsDic[name];
            return window;
        }

        /// <summary>
        /// 打开window
        /// </summary>
        /// <param name="name">window名字</param>
        public WindowBase openWindow(GameConfiger game, string name)
        {
            if (!windowsDic.ContainsKey(name))
            {
                preLoadWindow(game, name);
            }

            WindowBase window = windowsDic[name];
            window.show();
            return window;
        }



        /// <summary>
        /// 打开widget
        /// </summary>
        /// <param name="name">widget名字</param>
        public WidgetBase newWidget(GameConfiger game, string name, params object[] parameters)
        {
            if (!widgetsDic.ContainsKey(name))
            {
                preLoadWidget(game, name);
            }

            int count = widgetsPool[name].Count;
            if (count <= 1)
            {
                GameObject widgetPrefab = widgetsPool[name].Dequeue();
                GameObject widgetClone = Instantiate(widgetPrefab);
                widgetsPool[name].Enqueue(widgetPrefab);
                widgetsPool[name].Enqueue(widgetClone);
            }

            WidgetBase widget = widgetsPool[name].Dequeue().GetComponent<WidgetBase>();
            widget.show(parameters);
            return widget;
        }

        /// <summary>
        /// 回收widget
        /// </summary>
        public void destroyWidget(WidgetBase widget)
        {
            //TODO 重置状态
            //...

            GameObject widgetPrefab = widget.root().gameObject;
            widgetPrefab.SetActive(false);
            widgetsPool[name].Enqueue(widgetPrefab);
        }

    }
}