using UnityEngine;
using System.Collections;

public class CameraFollowTarget : MonoBehaviour
{
    public const float FOLLOW_SPEED = 3.0f;
    [SerializeField]
    private Transform target;

	void LateUpdate () 
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * FOLLOW_SPEED);
        }
	}

    public void SetTarget(Transform target, bool startOnTarget)
    {
        this.target = target;

        if (target != null && startOnTarget)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
