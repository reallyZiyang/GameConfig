using UnityEngine;

public class GameManager : GameBase.GameManager
{
    protected override void Start()
    {
        base.Start();
        GameBase.UIManager.Instance.openWindow(GameConfiger.Tetris, "GameMain");
        GameBase.WindowBase UI_gameMain = GameBase.UIManager.Instance.openWindow(GameConfiger.Tetris, "UI_GameMain");
    }

}
