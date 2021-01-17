using GameBase;

public class UI_GameMainWindow : WindowBase
{
    public override void init()
    {
        base.init();
        WidgetBase UI_Number = UIManager.Instance.newWidget(GameConfiger.Game_base, "UI_Number", 0);
    }
}
