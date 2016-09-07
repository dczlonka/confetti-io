using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent (typeof (Image))]
public class TouchInput : AbstractInput, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private RectTransform joystickBackground;

    [SerializeField]
    private RectTransform joystick;

    [SerializeField]
    private Button splitButton;

    private int joystickFingerId;
    private bool isJoystickActive;
    private Vector2 touchStart;
    private float joystickMaxDistance;
    private bool isActive;

    public override void Init()
    {
        // Calculate distance using world background corner positions. It should look same witch any resolution.
        Vector3[] corners = new Vector3[4];
        joystickBackground.GetWorldCorners(corners);
        float worldWidth = corners[2].x - corners[0].x;
        joystickMaxDistance = worldWidth / 2;

        splitButton.onClick.AddListener(OnSplitClicked);
        Disable();
    }

    public override void Enable ()
    {
        isActive = true;
        transform.parent.gameObject.SetActive(true);
        HideJoystick();
    }

    public override void Disable ()
    {
        isActive = false;
        transform.parent.gameObject.SetActive(false);
        SetAxisValue(Vector3.zero);
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        if (isJoystickActive == false)
        {
            isJoystickActive = true;
            joystickFingerId = eventData.pointerId;
            SetAxisValue(Vector2.zero);
            ShowJoystick(eventData.position);
        }
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        if (isJoystickActive == true && joystickFingerId == eventData.pointerId)
        {
            isJoystickActive = false;
            SetAxisValue(Vector2.zero);
            HideJoystick();
        }
    }

    public void OnDrag (PointerEventData eventData)
    {
        if (isJoystickActive == true && joystickFingerId == eventData.pointerId)
        {
            Vector2 dir = eventData.position - touchStart;
            if (dir.magnitude > INPUT_DEAD_ZONE)
            {
                SetAxisValue(dir.normalized);
            }
            else
            {
                SetAxisValue(Vector2.zero);
            }

            UpdateJoystickPosition(eventData.position);
        }
    }

    private void ShowJoystick(Vector2 start)
    {
        touchStart = start;
        joystickBackground.position = start;
        joystick.localPosition = Vector3.zero;
        joystickBackground.gameObject.SetActive(true);
    }

    private void HideJoystick()
    {
        joystickBackground.gameObject.SetActive(false);
    }

    private void UpdateJoystickPosition(Vector2 current)
    {
        joystick.position = Vector2.MoveTowards(joystickBackground.position, current, joystickMaxDistance);
    }
}


//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using System.Collections;
//
//public class TouchInput : MonoBehaviour
//{
//    private const int NO_FINGER_ID = -1;
//
//    [SerializeField]
//    private RectTransform joystickBackground;
//
//    [SerializeField]
//    private RectTransform joystick;
//
//    [SerializeField]
//    private Text label; //TODO test only
//
//    private int joystickFingerId = NO_FINGER_ID;
//    private bool isJoystickActive;
//    private Vector2 touchStart;
//
//    void Update()
//    {
//        foreach (var touch in Input.touches)
//        {
//            if (touch.phase == TouchPhase.Began)
//            {
//                OnFingerDown(touch.fingerId, touch.position);
//            }
//            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
//            {
//                OnFingerUp(touch.fingerId, touch.position);
//            }
//            else if (touch.phase == TouchPhase.Moved)
//            {
//                OnFingerMove(touch.fingerId, touch.position);
//            }
//        }
//    }
//
//    protected void OnFingerDown(int fingerId, Vector2 position)
//    {
//        if (isJoystickActive == false && !UiCollisionTest())
//        {
//            isJoystickActive = true;
//            joystickFingerId = fingerId;
//            SetAxisValue(Vector2.zero);
//            SetStartPosition(position);
//        }
//    }
//
//    protected void OnFingerUp(int fingerId, Vector2 position)
//    {
//        if (isJoystickActive == true && joystickFingerId == fingerId)
//        {
//            isJoystickActive = false;
//            joystickFingerId = NO_FINGER_ID;
//            SetAxisValue(Vector2.zero);
//        }
//    }
//
//    protected void OnFingerMove(int fingerId, Vector2 position)
//    {
//        if (isJoystickActive == true && joystickFingerId == fingerId)
//        {
//            Vector2 dir = position - touchStart;
//            if (dir.magnitude > GameInput.INPUT_DEAD_ZONE)
//            {
//                SetAxisValue(dir.normalized);
//            }
//            else
//            {
//                SetAxisValue(Vector2.zero);
//            }
//
//            UpdateJoystickPosition(position);
//        }
//    }
//
//    private void SetAxisValue(Vector2 axis)
//    {
//        label.text = "Axis: " + axis;
//        GameInput.Axis = axis;
//    }
//
//    private void SetStartPosition(Vector2 start)
//    {
//        touchStart = start;
//        joystickBackground.position = start;
//        joystick.localPosition = Vector3.zero;
//    }
//
//    private void UpdateJoystickPosition(Vector2 current)
//    {
//        joystick.position = Vector2.MoveTowards(joystickBackground.position, current, joystickBackground.rect.width);
//    }
//
//    /*
//     * If true, UI was clicked.
//     */
//    public bool UiCollisionTest()
//    {
//        return EventSystem.current.IsPointerOverGameObject();
//    }
//}
