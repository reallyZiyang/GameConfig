public class Events
{
    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="eventName"></param>
    public virtual void defineEvent()
    {
        EventDefine.registerEvent("GAME_START");
        EventDefine.registerEvent("GAME_OVER");
    }
}