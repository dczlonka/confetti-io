using UnityEngine;
using System.Collections;

public abstract class AbstractInput : MonoBehaviour
{
    public const float INPUT_DEAD_ZONE = 30f;

    public abstract void Init();
    public abstract void Enable();
    public abstract void Disable();

    void Awake()
    {
        Init();
    }

    protected void SetAxisValue(Vector2 axis)
    {
        GameInput.Axis = axis;
    }
}
