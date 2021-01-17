using UnityEngine;

namespace GameBase
{
    public class GameManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Events events = new Events();
            events.defineEvent();

            EventDefine.subscribeEvent(this, "GAME_START", new System.Action<object[]>((parameters) => { gameStart((System.DateTime)parameters[0]); }));
            EventDefine.subscribeEvent(this, "GAME_OVER", new System.Action<object[]>((parameters) => { gameOver((System.DateTime)parameters[0]); }));
            
            //添加UIManager组件
            if (FindObjectOfType<UIManager>() == null)
            {
                new GameObject("GameManager").AddComponent<UIManager>();
            }
            //添加AssetManager组件
            if (FindObjectOfType<AssetManager>() == null)
            {
                new GameObject("AssetManager").AddComponent<AssetManager>();
            }
        }

        protected virtual void Start()
        {
            EventDefine.CallEvent("GAME_START", System.DateTime.Now);
        }

        protected virtual void gameStart(System.DateTime curTime)
        {
            Debug.Log("<color=#FFFF00>" + "GAME_START:" + curTime + "</color>");
        }

        protected virtual void gameOver(System.DateTime curTime)
        {
            Debug.Log("<color=#FFFF00>" + "GAME_OVER:" + curTime + "</color>");
        }
    }
}
