using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class WidgetBase : UIBase
    {
        /// <summary>
        /// 销毁
        /// </summary>
        public void destroy()
        {
            onDestroy();
            UIManager.Instance.destroyWidget(this);
        }

        /// <summary>
        /// 关闭时调用
        /// </summary>
        protected void onDestroy() { }
    }
}

