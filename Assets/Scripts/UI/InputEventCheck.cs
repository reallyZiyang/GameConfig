using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputEventCheck : MonoBehaviour
{
    Object worldCastHit;
    Object UICastHit;

    private void Awake()
    {
        if (Object.FindObjectOfType<InputEventCheck>() != this)
        {
            Destroy(this);
            return;
        }

        eventSystem = Object.FindObjectOfType<EventSystem>();
        gra = Object.FindObjectOfType<GraphicRaycaster>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            worldCastHit = mouseWorldRaycast();
            UICastHit = mouseUIRaycast();
        }
    }

    private void LateUpdate()
    {
        if (UICastHit != null)
        {
            EventDefine.CallEvent(UIEvent.EVENT_TOUCH_DOWN, UICastHit, Input.mousePosition);
        }
        else if (worldCastHit != null)
        {
            EventDefine.CallEvent(UIEvent.EVENT_TOUCH_DOWN, worldCastHit, Input.mousePosition);
        }

        worldCastHit = null;
        UICastHit = null;
    }

    private Vector3 rayDir = new Vector3(0, 0, 1);
    private Object mouseWorldRaycast()
    {
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), rayDir * 100);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private EventSystem eventSystem;
    private GraphicRaycaster gra;
    private Object mouseUIRaycast()
    {
        var mPointerEventData = new PointerEventData(eventSystem);
        mPointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        gra.Raycast(mPointerEventData, results);
        return results.Count > 0 ? results[0].gameObject : null;
    }

    private void OnMouseDown()
    {
        //EventDefine.CallEvent(UIEvent.EVENT_TOUCH_DOWN, mouseRaycast(), Input.mousePosition);
    }

}