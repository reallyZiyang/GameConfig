public class GameManager : GameBase.GameManager
{

    protected override void Start()
    {
        base.Start();
        GameBase.UIManager.Instance.getWindow(GameConfiger.Tetris, "GameMain");
    }

}
