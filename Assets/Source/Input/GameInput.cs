using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class GameInput : Singleton<GameInput>
{
    [SerializeField]
    private HardwareInput hardwareInput;

    [SerializeField]
    private TouchInput touchInput;

    public static Vector3 Axis { get; set; }
    private bool isActive;
    private AbstractInput currentInput;

    void Awake ()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            currentInput = touchInput;
        }
        else
        {
            currentInput = hardwareInput;
        }
    }

    public void Enable ()
    {
        if (isActive)
            return;

        isActive = true;
        currentInput.Enable();
    }

    public void Disable ()
    {
        if (!isActive)
            return;

        isActive = false;
        currentInput.Disable();
    }

    public void OnSplitClicked ()
    {
        Debug.Log ("Split");
    }
}
