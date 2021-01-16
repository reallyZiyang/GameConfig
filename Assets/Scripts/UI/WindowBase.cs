using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBase : MonoBehaviour
{
    protected GameObject windowPrefab;

    /// <summary>
    /// 获取子集的某个物体
    /// </summary>
    /// <param name="childName">物体名字</param>
    /// <returns></returns>
    public Transform child(string childName)
    {
        return windowPrefab.transform.Find(childName);
    }

    /// <summary>
    /// 获取根节点
    /// </summary>
    /// <returns></returns>
    public Transform root()
    {
        return windowPrefab.transform;
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    public void show()
    {
        onShow();
        windowPrefab.SetActive(true);
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    public void close()
    {
        onClose();
        windowPrefab.SetActive(false);
    }

    /// <summary>
    /// 打开时调用
    /// </summary>
    protected void onShow() { }

    /// <summary>
    /// 关闭时调用
    /// </summary>
    protected void onClose() { }

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="eventName">事件</param>
    /// <param name="action">回调</param>
    public void subscribe(Transform child, string eventName, System.Action<object[]> func)
    {
        if (child == null || string.IsNullOrEmpty(eventName))
            return;
        
        EventDefine.subscribeEvent(child, eventName, func);
    }

    /// <summary>
    /// 取消事件
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="eventName">事件</param>
    public void unSubscribe(Transform child, string eventName)
    {
        if (child == null || string.IsNullOrEmpty(eventName))
            return;

        EventDefine.unSubscribeEvent(child, eventName);
    }

}
