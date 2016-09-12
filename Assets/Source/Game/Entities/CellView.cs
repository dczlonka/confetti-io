using UnityEngine;
using System.Collections;

public class CellView : EntityView
{
    public const float MAX_SPEED = 2.5f;
    public const float ACCELERATION = 5f;

    private Rigidbody2D rigid;
    private float speed = 0;

	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        if (GameInput.Axis != Vector3.zero)
        {
            // Accelerate
            speed = Mathf.Lerp(speed, MAX_SPEED, ACCELERATION * Time.deltaTime);
            rigid.velocity = GameInput.Axis * speed;
        }
        else
        {
            // Deccelerate
            speed = Mathf.Lerp(speed, 0, ACCELERATION * Time.deltaTime);
            rigid.velocity = rigid.velocity.normalized * speed;
        }
	}
}
