using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class AssetManager : MonoBehaviour
    {
        private static AssetManager instance;
        public static AssetManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<AssetManager>();
                return instance;
            }
        }

        private void Awake()
        {
            //manifest 加载依赖资源
            manifestAB = AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
            manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        private Dictionary<string, Object> assetsRef = new Dictionary<string, Object>();

        private AssetBundle manifestAB;
        private AssetBundleManifest manifest;

        private AssetBundle loadAsset(string gameName, string filePath, string prefabName)
        {
            string[] str = manifest.GetAllDependencies(filePath);

            foreach (string depend in str)
            {
                Debug.Log("<color=#FFFFFF>" + "加载[" + filePath + "]的依赖资源:" + depend + "</color>");
                AssetBundle.LoadFromFile("AssetBundles/" + depend);
            }

            Debug.Log("<color=#FFFFFF>" + "加载[" + filePath + "]" + "</color>");
            AssetBundle ab;
            try
            {
                //在自己目录下找
                ab = AssetBundle.LoadFromFile("AssetBundles/" + filePath);
                return ab;
            }
            catch (System.Exception)
            {
                //在公共目录下找
                gameName = GameConfiger.Game_base.ToString().ToLower();
                filePath = gameName + "/" + prefabName;
                ab = AssetBundle.LoadFromFile("AssetBundles/" + filePath);
                return ab;
            }
        }

        public Sprite loadSpriteAsset(GameConfiger game, string name)
        {
            if (assetsRef.ContainsKey(name))
                return assetsRef[name] as Sprite;

            string gameName = game.ToString().ToLower();
            string prefabName = name.ToLower();

            string filePath = gameName + "/" + prefabName;
            
            AssetBundle spriteAB = loadAsset(gameName, filePath, prefabName);
            string[] str = prefabName.Split('/');
            prefabName = str[str.Length - 1];
            Texture2D texture2D = spriteAB.LoadAsset<Texture2D>(prefabName);
            Sprite sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            assetsRef.Add(name, sprite);
            return sprite;
        }

        public GameObject loadWindowAsset(GameConfiger game, string name)
        {
            string gameName = game.ToString().ToLower();
            string prefabName = name.ToLower();

            string filePath = gameName + "/" + prefabName;

            AssetBundle windowAB = loadAsset(gameName, filePath, prefabName);
            GameObject windowPrefab = windowAB.LoadAsset<GameObject>(prefabName);
            return windowPrefab;
        }

    }
}