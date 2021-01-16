using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Events events = new Events();
        events.defineEvent();

        EventDefine.subscribeEvent(this, "GAME_START", new System.Action<object[]>((parameters) => { gameStart((System.DateTime)parameters[0]); }));
        EventDefine.subscribeEvent(this, "GAME_OVER", new System.Action<object[]>((parameters) => { gameOver((System.DateTime)parameters[0]); }));
    }

    private void Start()
    {
        EventDefine.CallEvent("GAME_START", System.DateTime.Now);
    }

    protected virtual void gameStart(System.DateTime curTime)
    {
        Debug.Log("GAME_START:" + curTime);
    }

    protected virtual void gameOver(System.DateTime curTime)
    {
        Debug.Log("GAME_OVER:" + curTime);
    }
}
