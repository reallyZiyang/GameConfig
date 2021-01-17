namespace GameBase
{
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

            var UIEvents = System.Enum.GetValues(typeof(UIEvent));
            foreach (var UIEvent in UIEvents)
            {
                EventDefine.registerEvent(UIEvent.ToString());
            }
        }
    }
}