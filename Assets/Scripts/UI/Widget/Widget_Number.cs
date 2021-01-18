using UnityEngine.UI;
using UnityEngine;

namespace GameBase
{
    public class Widget_Number : WidgetBase
    {
        private Image img;

        public override void init()
        {
            base.init();
        }

        protected override void onShow(params object[] parameters)
        {
            base.onShow(parameters);
            img = GetComponent<Image>();
            img.sprite = AssetManager.Instance.loadSpriteAsset(GameConfiger.Game_base, "number_sprites/number_" + (int)parameters[0]);
        }


    }
}

