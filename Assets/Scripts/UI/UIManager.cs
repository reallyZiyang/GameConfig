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
            GameObject windowPrefab = loadAsset(game, name);
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
                GameObject widgetPrefab = loadAsset(game, name);
                widgetsDic.Add(name, widgetPrefab.GetComponent<WidgetBase>());
                if (!widgetsPool.ContainsKey(name))
                    widgetsPool.Add(name, new Queue<GameObject>());
                widgetsPool[name].Enqueue(widgetPrefab);
            }
        }

        private GameObject loadAsset(GameConfiger game, string name)
        {
            bool isUI = name.Substring(0, 2).ToLower() == "ui";

            if (manifest == null || manifestAB == null)
            {
                //manifest 加载依赖资源
                manifestAB = AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
                manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }

            string gameName = game.ToString().ToLower();
            string prefabName = name.ToLower();

            string filePath = gameName + "/" + prefabName;

            string[] str;
            try
            {
                //在自己目录下找
                str = manifest.GetAllDependencies(filePath);
            }
            catch (System.Exception)
            {
                //在公共目录下找
                gameName = GameConfiger.Game_base.ToString().ToLower();
                filePath = gameName + "/" + prefabName;
                str = manifest.GetAllDependencies(filePath);
            }

            foreach (string depend in str)
            {
                Debug.Log("<color=#FFFFFF>" + "加载[" + filePath + "]的依赖资源:" + depend + "</color>");
                AssetBundle.LoadFromFile("Assets/AssetBundles/" + depend);
            }

            Debug.Log("<color=#FFFFFF>" + "加载[" + filePath + "]" + "</color>");
            AssetBundle windowAB = AssetBundle.LoadFromFile("AssetBundles/" + filePath);
            GameObject windowPrefab = windowAB.LoadAsset<GameObject>(prefabName);
            windowPrefab = Instantiate(windowPrefab, isUI ? CanvasTrans.Root : null, false);
            windowPrefab.SetActive(false);
            return windowPrefab;
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

            if (widgetsPool[name].Count <= 1)
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