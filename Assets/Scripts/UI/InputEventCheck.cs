using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameBase
{
    public class InputEventCheck : MonoBehaviour
    {
        bool mouseClick = false;
        private void Awake()
        {
            if (FindObjectOfType<InputEventCheck>() != this)
            {
                Destroy(this);
                return;
            }

            eventSystem = FindObjectOfType<EventSystem>();
            gra = FindObjectOfType<GraphicRaycaster>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseClick = true;
            }
        }

        private void LateUpdate()
        {
            if (mouseClick)
            {
                mouseWorldRaycast();
                mouseUIRaycast();
                mouseClick = false;
            }

        }

        private Vector3 rayDir = new Vector3(0, 0, 1);
        private void mouseWorldRaycast()
        {
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), rayDir * 100);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider)
                EventDefine.CallEvent(EntityEvent.EVENT_TOUCH_DOWN, hit.collider.gameObject, hit.collider);
        }

        private EventSystem eventSystem;
        private GraphicRaycaster gra;
        private void mouseUIRaycast()
        {
            var mPointerEventData = new PointerEventData(eventSystem);
            mPointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            gra.Raycast(mPointerEventData, results);
            if (results.Count > 0)
                EventDefine.CallEvent(UIEvent.EVENT_TOUCH_DOWN, results[0].gameObject, Input.mousePosition);
        }

        private void OnMouseDown()
        {
            //EventDefine.CallEvent(UIEvent.EVENT_TOUCH_DOWN, mouseRaycast(), Input.mousePosition);
        }

    }
}
