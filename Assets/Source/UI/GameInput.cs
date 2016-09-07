using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class GameInput : InputPanelBase {

    public const float INPUT_DEAD_ZONE = 30f;
    private const int NO_FINGER_ID = -1;

    public static Vector3 Axis { get; set; }

    private int joystickFingerId = NO_FINGER_ID;
    private bool isJoystickActive;
    private Vector3 screenCenter;

    [SerializeField]
    private Text label;

//    public void Enable()
//    {
//
//    }
//
//    public void Disable()
//    {
//
//    }
//
//    public static Vector2 GetAxis()
//    {
//        return Vector2.zero;
//    }
//
//    void Start()
//    {
//        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
//    }
//
//    void Update()
//    {
//        #if MOBILE_INPUT
//        Debug.Log("Mobile Input");
//        #else
//        UpdateMouseInput();
//        #endif
//
//        label.text = "Axis: " + axis;
//    }
//
//    private void UpdateMouseInput()
//    {
//        Vector3 dir = Input.mousePosition - screenCenter;
//        if (dir.magnitude > INPUT_DEAD_ZONE)
//        {
//            axis = dir.normalized;
//        }
//        else
//        {
//            axis = Vector3.zero;
//        }
//    }
//
//    public void OnSplitClicked()
//    {
//        Debug.Log("Split");
//    }

//    protected override void OnFingerDown(int fingerId, Vector2 position) {
//        if (isJoystickActive == false)
//        {
//            isJoystickActive = true;
//            joystickFingerId = fingerId;
//            Debug.Log("Down finger: " + fingerId);
//            label.text = "Position: " + position;
//        }
//    }
//
//    protected override void OnFingerUp(int fingerId, Vector2 position) {
//        if (isJoystickActive == true && joystickFingerId == fingerId)
//        {
//            isJoystickActive = false;
//            joystickFingerId = NO_FINGER_ID;
//            Debug.Log("Up finger: " + fingerId);
//            label.text = "Position: " + position;
//        }
//    }
//
//    protected override void OnFingerMove(int fingerId, Vector2 position) {
//        if (isJoystickActive == true && joystickFingerId == fingerId)
//        {
//            Debug.Log("Move finger: " + fingerId);
//            label.text = "Position: " + position;
//        }
//    }

    private void Reset()
    {
        isJoystickActive = false;
        joystickFingerId = NO_FINGER_ID;
    }


}
