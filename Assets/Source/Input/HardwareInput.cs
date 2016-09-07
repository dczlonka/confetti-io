using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class HardwareInput : AbstractInput
{
    private Vector3 screenCenter;
    private bool isActive;

    public override void Init ()
    {
        screenCenter = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
        Disable();
    }

    public override void Enable ()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public override void Disable ()
    {
        isActive = false;
        SetAxisValue(Vector3.zero);
        gameObject.SetActive(false);
    }

    void Update ()
    {
        if (!isActive)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSplitClicked();
        }

        Vector3 dir = Input.mousePosition - screenCenter;
        if (dir.magnitude > INPUT_DEAD_ZONE)
        {
            SetAxisValue(dir.normalized);
        }
        else
        {
            SetAxisValue(Vector3.zero);
        }
    }
}
