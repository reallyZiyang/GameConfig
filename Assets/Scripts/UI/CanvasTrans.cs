using UnityEngine;

namespace GameBase
{
    public static class CanvasTrans
    {
        private static Transform root;
        public static Transform Root
        {
            get
            {
                if (root == null)
                    root = GameObject.Find("Canvas").transform;
                return root;
            }
        }
    }
}