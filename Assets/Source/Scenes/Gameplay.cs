using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    void Start()
    {
        GameInput.Instance.Enable();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
