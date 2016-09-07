using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class InputPanelBase : MonoBehaviour {

    private const int MOUSE_BUTTON = 0;
    private const int MOUSE_FINGER_ID = 100; // Just high number

    private Vector3 lastMousePosition;

    void Update()
    {
        foreach (var item in Input.touches)
        {
            if (item.phase == TouchPhase.Began)
            {
                OnFingerDown(item.fingerId, item.position);
            }
            else if (item.phase == TouchPhase.Canceled || item.phase == TouchPhase.Ended)
            {
                OnFingerUp(item.fingerId, item.position);
            }
            else if (item.phase == TouchPhase.Moved)
            {
                OnFingerMove(item.fingerId, item.position);
            }
        }

//        if (Input.GetMouseButtonDown(MOUSE_BUTTON))
//        {
//            OnFingerDown(MOUSE_FINGER_ID, Input.mousePosition);
//            lastMousePosition = Input.mousePosition;
//        }
//        else if (Input.GetMouseButtonUp(MOUSE_BUTTON))
//        {
//            OnFingerUp(MOUSE_FINGER_ID, Input.mousePosition);
//            lastMousePosition = Input.mousePosition;
//        }
//        else if (Input.GetMouseButton(MOUSE_BUTTON))
//        {
//            if (lastMousePosition != Input.mousePosition)
//            {
//                OnFingerMove(MOUSE_FINGER_ID, Input.mousePosition);
//                lastMousePosition = Input.mousePosition;
//            }
//        }
    }

    protected virtual void OnFingerDown(int fingerId, Vector2 position) {

    }

    protected virtual void OnFingerUp(int fingerId, Vector2 position) {

    }

    protected virtual void OnFingerMove(int fingerId, Vector2 position) {

    }
}
