using UnityEngine;
using System.Collections;

public class GrabObject : MonoBehaviour
{
    public bool grabbed;
    private RaycastHit2D hit;
    public float distance;
    public Transform holdPoint;
    public float throwForce;
    public Transform rayCastPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!grabbed)
            {
                hit = Physics2D.Raycast(rayCastPoint.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "MoveObject")
                {
                    grabbed = true;
                }
            }
            else
            {
                grabbed = false;

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
                }
            }
        }
        if (grabbed)
        {
            hit.collider.gameObject.transform.position = holdPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}