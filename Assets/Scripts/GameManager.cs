using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Events events = new Events();
        events.defineEvent();

        EventDefine.subscribeEvent(this, "GAME_START", new System.Action(() => { gameStart(); }));
        EventDefine.subscribeEvent(this, "GAME_OVER", new System.Action(() => { gameOver(); }));
    }

    private void Start()
    {
        EventDefine.CallEvent("GAME_START");
    }

    protected virtual void gameStart()
    {
        Debug.Log("GAME_START");
    }

    protected virtual void gameOver()
    {
        Debug.Log("GAME_OVER");
    }
}
