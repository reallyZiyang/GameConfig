using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameConfiger
{
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

        /// <summary>
        /// 加载window
        /// </summary>
        /// <param name="name"></param>
        public void preLoadWindow(GameConfiger game, string name)
        {
            string gameName = game.ToString().ToLower();
            string prefabName = name.ToLower();

            string filePath = gameName + "/" + prefabName;
            //manifest 加载依赖资源
            AssetBundle manifestAB = AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
            AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] str = manifest.GetAllDependencies(filePath);
            foreach (string depend in str)
            {
                Debug.Log("<color=#FFFFFF>" + "加载[" + filePath + "]的依赖资源:" + depend + "</color>");
                AssetBundle.LoadFromFile("Assets/AssetBundles/" + depend);
            }

            Debug.Log("<color=#FFFFFF>" + "加载[" + filePath + "]" + "</color>");
            AssetBundle windowAB = AssetBundle.LoadFromFile("AssetBundles/" + filePath);
            GameObject windowPrefab = windowAB.LoadAsset<GameObject>(prefabName);
            Instantiate(windowPrefab);
            windowPrefab.SetActive(false);
            windowsDic.Add(name, windowPrefab.GetComponent<WindowBase>());
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
            //window.init();
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

    }
}