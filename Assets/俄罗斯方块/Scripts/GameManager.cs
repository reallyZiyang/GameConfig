using UnityEngine;

public class GameManager : GameBase.GameManager
{
    protected override void Start()
    {
        base.Start();
        GameBase.UIManager.Instance.openWindow(GameConfiger.Tetris, "Win_GameMain");
    }

}
