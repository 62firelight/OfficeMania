using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector2 velocity = Vector2.zero;

    void FixedUpdate()
    {
        Vector2 desiredPosition = target.position + offset;
        Vector2 smoothedPosition = Vector2.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);

        // transform.LookAt(target);
    }

}