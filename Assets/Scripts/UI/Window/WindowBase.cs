using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class WindowBase : UIBase
    {
        public void close()
        {
            onClose();
            UIPrefab.SetActive(false);
        }

        /// <summary>
        /// 关闭时调用
        /// </summary>
        protected void onClose() { }
    }
}

